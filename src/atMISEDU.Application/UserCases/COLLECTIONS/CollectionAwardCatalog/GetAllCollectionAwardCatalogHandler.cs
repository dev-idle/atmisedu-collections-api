using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionAwardCatalogRequest"/>
    /// </summary>
    public class GetAllCollectionAwardCatalogHandler : IRequestHandler<GetAllCollectionAwardCatalogRequest, Result<List<CollectionAwardCatalogResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionAwardCatalogHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionAwardCatalogHandler(ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo)
        {
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionAwardCatalogRequest"/>, GetAll the <see cref="Entities.CollectionAwardCatalog"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionAwardCatalog"/></returns>
        public async Task<Result<List<CollectionAwardCatalogResponse>>> Handle(GetAllCollectionAwardCatalogRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionAwardCatalogValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by isActived when it is sent
            var query = collectionAwardCatalogRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionAwardCatalog from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionAwardCatalogResponse.FromEntity).ToList();
            return Result<List<CollectionAwardCatalogResponse>>.Ok(data);
        }
    }
}
