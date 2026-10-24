using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Handler for <see cref="UpdateCollectionArticlesRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesHandler : IRequestHandler<UpdateCollectionArticlesRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesTags"/>>
        private readonly ICollectionArticlesTagsSqlRepository collectionArticlesTagsRepo;
        // Repository handle data access of <see cref="Entities.CollectionTags"/>>
        private readonly ICollectionTagsSqlRepository collectionTagsRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesHandler"/>, inject needed dependency
        /// </summary>
        public UpdateCollectionArticlesHandler(ICollectionArticlesSqlRepository collectionArticlesRepo,
                                               ICollectionArticlesTagsSqlRepository collectionArticlesTagsRepo,
                                               ICollectionTagsSqlRepository collectionTagsRepo,
                                               ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.collectionArticlesTagsRepo = collectionArticlesTagsRepo;
            this.collectionTagsRepo = collectionTagsRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="UpdateCollectionArticlesRequest"/>, Update the <see cref="Entities.CollectionArticles"/> base on data <see cref="UpdateCollectionArticlesRequest"/>,
        /// replace its links with tags when tag ids are sent and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(UpdateCollectionArticlesRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.ArticleTitle = request.ArticleTitle?.Trim();
            request.ArticleAbstract = request.ArticleAbstract?.Trim();
            // Only trim the ends of content, inner content is kept
            request.ArticleContent = request.ArticleContent?.Trim();
            request.ArticleThumbnail = request.ArticleThumbnail?.Trim();
            request.ArticleIPAddress = request.ArticleIPAddress?.Trim();
            request.ArticleBrowser = request.ArticleBrowser?.Trim();
            request.ArticleOstype = request.ArticleOstype?.Trim();
            var validator = new UpdateCollectionArticlesValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticles (not deleted) from database, throw NotFoundException when not found
            var collectionArticles = await collectionArticlesRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticles == null || collectionArticles.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticles), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_ID_NOT_FOUND);

            // Tag ids not sent (null) keep old links, empty list remove all links, otherwise replace links
            var tagIds = request.TagIds?.Distinct().ToList();
            var removedLinks = new List<Entities.CollectionArticlesTags>();
            var addedLinks = new List<Entities.CollectionArticlesTags>();
            if (tagIds != null)
            {
                // Every tag must exist and not be deleted, throw NotFoundException when not found
                foreach (var tagId in tagIds)
                {
                    if (!await collectionTagsRepo.IsExistAsync(x => x.Id == tagId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                        CustomException.ThrowNotFoundException(typeof(Entities.CollectionTags), MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_ID_NOT_FOUND);
                }

                // Replace by diff: remove old links not in new list, add new links not in old list
                // (never remove and add the same link in one save to avoid tracking conflict)
                var articleId = collectionArticles.Id;
                var oldLinks = collectionArticlesTagsRepo.FindAll(x => x.ArticleId == articleId).ToList();
                var oldKeyTags = oldLinks.Select(x => x.KeyTags).ToList();
                removedLinks = oldLinks.Where(x => !tagIds.Contains(x.KeyTags)).ToList();
                addedLinks = tagIds.Where(tagId => !oldKeyTags.Contains(tagId))
                                   .Select(tagId => new Entities.CollectionArticlesTags
                                   {
                                       ArticleId = articleId,
                                       KeyTags = tagId
                                   }).ToList();
            }

            // Only fields sent with value are updated, null fields keep old value
            request.MapTo(collectionArticles, true);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticles as Update state
                collectionArticlesRepo.Update(collectionArticles);
                // Marked removed links as Delete state and new links as Create state
                if (removedLinks.Any())
                    collectionArticlesTagsRepo.RemoveMultiple(removedLinks);
                if (addedLinks.Any())
                    collectionArticlesTagsRepo.AddRange(addedLinks);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok();
            }
            catch (Exception)
            {
                // Rollback transaction if any exception happened, then throw exception
                transaction.Rollback();
                throw;
            }
        }
    }
}
