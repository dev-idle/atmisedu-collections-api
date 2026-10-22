using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Application.Validators.CATALOG.CatalogUnit;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG;
using MediatR;
using Entity = atMISEDU.Domain.Entities.CATALOG;

namespace atMISEDU.Application.UserCases.CATALOG.CatalogUnit
{
    /// <summary>
    /// Handler for <see cref="DeleteCatalogUnitRequest"/> 
    /// </summary>
    public class DeleteCatalogUnitHandler : IRequestHandler<DeleteCatalogUnitRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entity.CatalogUnit"/>>
        private readonly ICatalogUnitSqlRepository catalogUnitRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCatalogUnitHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCatalogUnitHandler(ICatalogUnitSqlRepository catalogUnitRepo, ISqlUnitOfWork unitOfWork)
        {
            this.catalogUnitRepo = catalogUnitRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCatalogUnitRequest"/>, Delete the <see cref="Entity.CatalogUnit"/> base on data <see cref="DeleteCatalogUnitRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCatalogUnitRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCatalogUnitValidator();
            validator.ValidateAndThrow(request);

            // Find CatalogUnit from database, throw NotFoundException when not found
            var catalogUnit = await catalogUnitRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (catalogUnit == null)
                CustomException.ThrowNotFoundException(typeof(Entity.CatalogUnit), MsgCode.ERR_CATALOG_CATALOG_UNIT_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CatalogUnit as Delete state
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
