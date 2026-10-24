using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionAwardCatalogRequest"/>
    /// </summary>
    public class GetDetailCollectionAwardCatalogHandler : IRequestHandler<GetDetailCollectionAwardCatalogRequest, Result<CollectionAwardCatalogResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionAwardCatalogHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionAwardCatalogHandler(ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo)
        {
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionAwardCatalogRequest"/>, GetDetail the <see cref="Entities.CollectionAwardCatalog"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionAwardCatalog"/></returns>
        public async Task<Result<CollectionAwardCatalogResponse>> Handle(GetDetailCollectionAwardCatalogRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionAwardCatalogValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionAwardCatalog (not deleted) from database, throw NotFoundException when not found
            var collectionAwardCatalog = collectionAwardCatalogRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionAwardCatalog == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionAwardCatalog), MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_ID_NOT_FOUND);
            }
            return Result<CollectionAwardCatalogResponse>.Ok(CollectionAwardCatalogResponse.FromEntity(collectionAwardCatalog));
        }
    }
}
