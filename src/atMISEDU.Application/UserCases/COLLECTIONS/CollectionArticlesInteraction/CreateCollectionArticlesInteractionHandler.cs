using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Handler for <see cref="CreateCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class CreateCollectionArticlesInteractionHandler : IRequestHandler<CreateCollectionArticlesInteractionRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesInteraction"/>>
        private readonly ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesInteractionHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionArticlesInteractionHandler(ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo, ICollectionArticlesSqlRepository collectionArticlesRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesInteractionRepo = collectionArticlesInteractionRepo;
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionArticlesInteractionRequest"/>, Create the <see cref="Entities.CollectionArticlesInteraction"/> base on data <see cref="CreateCollectionArticlesInteractionRequest"/>
        /// and save to database. If same user already interacted with same article (include deleted record), reuse that record instead of creating new one
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created (or reused) <see cref="Entities.CollectionArticlesInteraction"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionArticlesInteractionRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.IntnType = request.IntnType?.Trim();
            var validator = new CreateCollectionArticlesInteractionValidator();
            validator.ValidateAndThrow(request);

            // Check CollectionArticles (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionArticlesRepo.IsExistAsync(x => x.Id == request.ArticleId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticles), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_ID_NOT_FOUND);

            // Find CollectionArticlesInteraction of same user on same article (include deleted record) to reuse
            var collectionArticlesInteraction = await collectionArticlesInteractionRepo.FindSingleAsync(x => x.ArticleId == request.ArticleId && x.CreatedBy == request.CreatedBy, true, cancellationToken);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                if (collectionArticlesInteraction != null)
                {
                    // Same user post again, update interaction type and status, keep id and createdDate
                    collectionArticlesInteraction.IntnType = request.IntnType!;
                    collectionArticlesInteraction.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;
                    // Marked CollectionArticlesInteraction as Update state
                    collectionArticlesInteractionRepo.Update(collectionArticlesInteraction);
                }
                else
                {
                    // Create new CollectionArticlesInteraction from request, apply default values
                    collectionArticlesInteraction = request.MapTo<Entities.CollectionArticlesInteraction>()!;
                    collectionArticlesInteraction.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    collectionArticlesInteraction.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;
                    // Marked CollectionArticlesInteraction as Create state
                    collectionArticlesInteractionRepo.Add(collectionArticlesInteraction);
                }
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { interactionId = collectionArticlesInteraction.Id });
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
