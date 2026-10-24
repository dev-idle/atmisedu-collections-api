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
    /// Handler for <see cref="UpdateCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesInteractionHandler : IRequestHandler<UpdateCollectionArticlesInteractionRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesInteraction"/>>
        private readonly ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesInteractionHandler"/>, inject needed dependency
        /// </summary>
        public UpdateCollectionArticlesInteractionHandler(ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesInteractionRepo = collectionArticlesInteractionRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="UpdateCollectionArticlesInteractionRequest"/>, Update the <see cref="Entities.CollectionArticlesInteraction"/> base on data <see cref="UpdateCollectionArticlesInteractionRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(UpdateCollectionArticlesInteractionRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.IntnType = request.IntnType?.Trim();
            var validator = new UpdateCollectionArticlesInteractionValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesInteraction (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesInteraction = await collectionArticlesInteractionRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesInteraction == null || collectionArticlesInteraction.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesInteraction), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INTERACTION_ID_NOT_FOUND);
            // Only fields sent with value are updated, null fields keep old value
            request.MapTo(collectionArticlesInteraction, true);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesInteraction as Update state
                collectionArticlesInteractionRepo.Update(collectionArticlesInteraction);
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
