using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Handler for <see cref="CreateCollectionAwardCatalogRequest"/>
    /// </summary>
    public class CreateCollectionAwardCatalogHandler : IRequestHandler<CreateCollectionAwardCatalogRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionAwardCatalogHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionAwardCatalogHandler(ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionAwardCatalogRequest"/>, Create the <see cref="Entities.CollectionAwardCatalog"/> base on data <see cref="CreateCollectionAwardCatalogRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created <see cref="Entities.CollectionAwardCatalog"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionAwardCatalogRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.AwIcon = request.AwIcon?.Trim();
            var validator = new CreateCollectionAwardCatalogValidator();
            validator.ValidateAndThrow(request);

            // Create new CollectionAwardCatalog from request, apply default values
            var collectionAwardCatalog = request.MapTo<Entities.CollectionAwardCatalog>();
            collectionAwardCatalog.AwPoint = request.AwPoint ?? 0;
            collectionAwardCatalog.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionAwardCatalog as Create state
                collectionAwardCatalogRepo.Add(collectionAwardCatalog);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { awardId = collectionAwardCatalog.Id });
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
