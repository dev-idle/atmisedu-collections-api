using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Application.Validators.CATALOG.CatalogUnit;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG;
using MediatR;
using Entity = atMISEDU.Domain.Entities.CATALOG;

namespace atMISEDU.Application.UserCases.CATALOG.CatalogUnit
{
    /// <summary>
    /// Handler for <see cref="GetDetailCatalogUnitRequest"/>/ 
    /// </summary>
    public class GetDetailCatalogUnitHandler : IRequestHandler<GetDetailCatalogUnitRequest, Result<Entity.CatalogUnit>>
    {
        // Repository handle data access of <see cref="Entity.CatalogUnit"/>>
        private readonly ICatalogUnitSqlRepository catalogUnitRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCatalogUnitHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCatalogUnitHandler(ICatalogUnitSqlRepository catalogUnitRepo)
        {
            this.catalogUnitRepo = catalogUnitRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCatalogUnitRequest"/>, GetDetail the <see cref="Entity.CatalogUnit"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entity.CatalogUnit"/></returns>
        public async Task<Result<Entity.CatalogUnit>> Handle(GetDetailCatalogUnitRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCatalogUnitValidator();
            validator.ValidateAndThrow(request);

            // Find CatalogUnit from database, throw NotFoundException when not found
            var catalogUnit = catalogUnitRepo.FindAll(x => x.Id.Equals(request.Id)).FirstOrDefault();
            if (catalogUnit == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entity.CatalogUnit), MsgCode.ERR_CATALOG_CATALOG_UNIT_ID_NOT_FOUND);
            }
            return catalogUnit;
        }
    }
}
