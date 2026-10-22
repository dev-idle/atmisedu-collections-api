using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Application.Validators.CATALOG.CatalogUnit;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG;
using MediatR;
using Entity = atMISEDU.Domain.Entities.CATALOG;

namespace atMISEDU.Application.UserCases.CATALOG.CatalogUnit
{
    /// <summary>
    /// Handler for <see cref="CreateCatalogUnitRequest"/> 
    /// </summary>
    public class CreateCatalogUnitHandler : IRequestHandler<CreateCatalogUnitRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entity.CatalogUnit"/>>
        private readonly ICatalogUnitSqlRepository catalogUnitRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCatalogUnitHandler"/>, inject needed dependency
        /// </summary>
        public CreateCatalogUnitHandler(ICatalogUnitSqlRepository catalogUnitRepo, ISqlUnitOfWork unitOfWork)
        {
            this.catalogUnitRepo = catalogUnitRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCatalogUnitRequest"/>, Create the <see cref="Entity.CatalogUnit"/> base on data <see cref="CreateCatalogUnitRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCatalogUnitRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new CreateCatalogUnitValidator();
            validator.ValidateAndThrow(request);

            // Create new CatalogUnit from request
            var catalogUnit = request.MapTo<Entity.CatalogUnit>();

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CatalogUnit as Create state
                catalogUnitRepo.Add(catalogUnit);
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
