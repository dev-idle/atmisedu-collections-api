using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Handler for <see cref="DeleteCollectionTagsRequest"/>
    /// </summary>
    public class DeleteCollectionTagsHandler : IRequestHandler<DeleteCollectionTagsRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionTags"/>>
        private readonly ICollectionTagsSqlRepository collectionTagsRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionTagsHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionTagsHandler(ICollectionTagsSqlRepository collectionTagsRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionTagsRepo = collectionTagsRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionTagsRequest"/>, soft delete the <see cref="Entities.CollectionTags"/> base on data <see cref="DeleteCollectionTagsRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionTagsRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionTagsValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionTags (not deleted) from database, throw NotFoundException when not found
            var collectionTags = await collectionTagsRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionTags == null || collectionTags.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionTags), MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionTags as Delete state
                collectionTags.IsActived = (int)ActivedType.DELETE;
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
