using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Application.Validators.CATALOG.CatalogUnit;
using atMISEDU.Contract.DependencyInjection.Extensions;
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
    /// Handler for <see cref="UpdateCatalogUnitRequest"/> 
    /// </summary>
    public class UpdateCatalogUnitHandler : IRequestHandler<UpdateCatalogUnitRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entity.CatalogUnit"/>>
        private readonly ICatalogUnitSqlRepository catalogUnitRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="UpdateCatalogUnitHandler"/>, inject needed dependency
        /// </summary>
        public UpdateCatalogUnitHandler(ICatalogUnitSqlRepository catalogUnitRepo, ISqlUnitOfWork unitOfWork)
        {
           this.catalogUnitRepo = catalogUnitRepo;
           this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="UpdateCatalogUnitRequest"/>, Update the <see cref="Entity.CatalogUnit"/> base on data <see cref="UpdateCatalogUnitRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(UpdateCatalogUnitRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new UpdateCatalogUnitValidator();
            validator.ValidateAndThrow(request);

            // Find CatalogUnit from database, throw NotFoundException when not found
            var catalogUnit = await catalogUnitRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if(catalogUnit == null)
                CustomException.ThrowNotFoundException(typeof(Entity.CatalogUnit), MsgCode.ERR_CATALOG_CATALOG_UNIT_ID_NOT_FOUND);
            request.MapTo(catalogUnit, true);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CatalogUnit as Update state
                catalogUnitRepo.Update(catalogUnit);
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
