using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG;
using MediatR;
using Entity = atMISEDU.Domain.Entities.CATALOG;

namespace atMISEDU.Application.UserCases.CATALOG.CatalogUnit
{
    /// <summary>
    /// Handler for <see cref="GetAllCatalogUnitRequest"/>/ 
    /// </summary>
    public class GetAllCatalogUnitHandler : IRequestHandler<GetAllCatalogUnitRequest, Result<IQueryable<Entity.CatalogUnit>>>
    {
        // Repository handle data access of <see cref="Entity.CatalogUnit"/>>
        private readonly ICatalogUnitSqlRepository catalogUnitRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCatalogUnitHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCatalogUnitHandler(ICatalogUnitSqlRepository catalogUnitRepo)
        {
            this.catalogUnitRepo = catalogUnitRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCatalogUnitRequest"/>, GetAll the <see cref="Entity.CatalogUnit"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entity.CatalogUnit"/></returns>
        public async Task<Result<IQueryable<Entity.CatalogUnit>>> Handle(GetAllCatalogUnitRequest request, CancellationToken cancellationToken)
        {
            // Find CatalogUnit from database and return
            var data = catalogUnitRepo.FindAll().AsQueryable();
            return Result<IQueryable<Entity.CatalogUnit>>.Ok(data);
        }
    }
}
