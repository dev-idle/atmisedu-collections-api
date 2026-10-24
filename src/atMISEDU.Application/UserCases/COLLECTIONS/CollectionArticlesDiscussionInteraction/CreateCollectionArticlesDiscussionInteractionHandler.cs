using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Handler for <see cref="CreateCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionInteractionHandler : IRequestHandler<CreateCollectionArticlesDiscussionInteractionRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionInteraction"/>>
        private readonly ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesDiscussionInteractionHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionArticlesDiscussionInteractionHandler(ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo, ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionInteractionRepo = collectionArticlesDiscussionInteractionRepo;
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionArticlesDiscussionInteractionRequest"/>, Create the <see cref="Entities.CollectionArticlesDiscussionInteraction"/> base on data <see cref="CreateCollectionArticlesDiscussionInteractionRequest"/>
        /// and save to database. If same user already interacted with same discussion (include deleted record), reuse that record instead of creating new one
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created (or reused) <see cref="Entities.CollectionArticlesDiscussionInteraction"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionArticlesDiscussionInteractionRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.IntnType = request.IntnType?.Trim();
            var validator = new CreateCollectionArticlesDiscussionInteractionValidator();
            validator.ValidateAndThrow(request);

            // Check CollectionArticlesDiscussion (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionArticlesDiscussionRepo.IsExistAsync(x => x.Id == request.DiscussionId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussion), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_ID_NOT_FOUND);

            // Find CollectionArticlesDiscussionInteraction of same user on same discussion (include deleted record) to reuse
            var collectionArticlesDiscussionInteraction = await collectionArticlesDiscussionInteractionRepo.FindSingleAsync(x => x.DiscussionId == request.DiscussionId && x.CreatedBy == request.CreatedBy, true, cancellationToken);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                if (collectionArticlesDiscussionInteraction != null)
                {
                    // Same user post again, update interaction type and status, keep id and createdDate
                    collectionArticlesDiscussionInteraction.IntnType = request.IntnType!;
                    collectionArticlesDiscussionInteraction.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;
                    // Marked CollectionArticlesDiscussionInteraction as Update state
                    collectionArticlesDiscussionInteractionRepo.Update(collectionArticlesDiscussionInteraction);
                }
                else
                {
                    // Create new CollectionArticlesDiscussionInteraction from request, apply default values
                    collectionArticlesDiscussionInteraction = request.MapTo<Entities.CollectionArticlesDiscussionInteraction>()!;
                    collectionArticlesDiscussionInteraction.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    collectionArticlesDiscussionInteraction.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;
                    // Marked CollectionArticlesDiscussionInteraction as Create state
                    collectionArticlesDiscussionInteractionRepo.Add(collectionArticlesDiscussionInteraction);
                }
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { interactionId = collectionArticlesDiscussionInteraction.Id });
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
