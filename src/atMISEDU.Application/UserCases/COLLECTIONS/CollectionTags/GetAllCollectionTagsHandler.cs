using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionTags;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionTagsRequest"/>
    /// </summary>
    public class GetAllCollectionTagsHandler : IRequestHandler<GetAllCollectionTagsRequest, Result<List<CollectionTagsResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionTags"/>>
        private readonly ICollectionTagsSqlRepository collectionTagsRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionTagsHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionTagsHandler(ICollectionTagsSqlRepository collectionTagsRepo)
        {
            this.collectionTagsRepo = collectionTagsRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionTagsRequest"/>, GetAll the <see cref="Entities.CollectionTags"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionTags"/></returns>
        public async Task<Result<List<CollectionTagsResponse>>> Handle(GetAllCollectionTagsRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionTagsValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by isActived when it is sent
            var query = collectionTagsRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionTags from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionTagsResponse.FromEntity).ToList();
            return Result<List<CollectionTagsResponse>>.Ok(data);
        }
    }
}
