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
    /// Handler for <see cref="CreateCollectionArticlesRequest"/>
    /// </summary>
    public class CreateCollectionArticlesHandler : IRequestHandler<CreateCollectionArticlesRequest, Result<object>>
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
        /// Constructor of <see cref="CreateCollectionArticlesHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionArticlesHandler(ICollectionArticlesSqlRepository collectionArticlesRepo,
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
        /// Handle <see cref="CreateCollectionArticlesRequest"/>, Create the <see cref="Entities.CollectionArticles"/> base on data <see cref="CreateCollectionArticlesRequest"/>,
        /// link it with tags and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created <see cref="Entities.CollectionArticles"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionArticlesRequest request, CancellationToken cancellationToken)
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
            var validator = new CreateCollectionArticlesValidator();
            validator.ValidateAndThrow(request);

            // Distinct tag ids, every tag must exist and not be deleted, throw NotFoundException when not found
            var tagIds = request.TagIds?.Distinct().ToList() ?? new List<int>();
            foreach (var tagId in tagIds)
            {
                if (!await collectionTagsRepo.IsExistAsync(x => x.Id == tagId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                    CustomException.ThrowNotFoundException(typeof(Entities.CollectionTags), MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_ID_NOT_FOUND);
            }

            // Create new CollectionArticles from request, apply default values
            var collectionArticles = request.MapTo<Entities.CollectionArticles>();
            collectionArticles.ArticleTimeReading = request.ArticleTimeReading ?? 0;
            collectionArticles.ArticleViews = request.ArticleViews ?? 0;
            collectionArticles.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            collectionArticles.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticles as Create state
                collectionArticlesRepo.Add(collectionArticles);
                // Save data to database to get id of created CollectionArticles
                await unitOfWork.SaveChangesAsync(cancellationToken);

                if (tagIds.Any())
                {
                    // Marked links between CollectionArticles and CollectionTags as Create state
                    var links = tagIds.Select(tagId => new Entities.CollectionArticlesTags
                    {
                        ArticleId = collectionArticles.Id,
                        KeyTags = tagId
                    }).ToList();
                    collectionArticlesTagsRepo.AddRange(links);
                    // Save data to database
                    await unitOfWork.SaveChangesAsync(cancellationToken);
                }

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { articleId = collectionArticles.Id });
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
