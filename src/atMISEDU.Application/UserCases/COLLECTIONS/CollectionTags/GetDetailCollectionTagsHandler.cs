using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionTags;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionTagsRequest"/>
    /// </summary>
    public class GetDetailCollectionTagsHandler : IRequestHandler<GetDetailCollectionTagsRequest, Result<CollectionTagsResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionTags"/>>
        private readonly ICollectionTagsSqlRepository collectionTagsRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionTagsHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionTagsHandler(ICollectionTagsSqlRepository collectionTagsRepo)
        {
            this.collectionTagsRepo = collectionTagsRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionTagsRequest"/>, GetDetail the <see cref="Entities.CollectionTags"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionTags"/></returns>
        public async Task<Result<CollectionTagsResponse>> Handle(GetDetailCollectionTagsRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionTagsValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionTags (not deleted) from database, throw NotFoundException when not found
            var collectionTags = collectionTagsRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionTags == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionTags), MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_ID_NOT_FOUND);
            }
            return Result<CollectionTagsResponse>.Ok(CollectionTagsResponse.FromEntity(collectionTags));
        }
    }
}
