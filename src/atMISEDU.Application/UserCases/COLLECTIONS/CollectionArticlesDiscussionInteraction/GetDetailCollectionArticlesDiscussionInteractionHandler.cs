using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionInteractionHandler : IRequestHandler<GetDetailCollectionArticlesDiscussionInteractionRequest, Result<CollectionArticlesDiscussionInteractionResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionInteraction"/>>
        private readonly ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionArticlesDiscussionInteractionHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionArticlesDiscussionInteractionHandler(ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo)
        {
            this.collectionArticlesDiscussionInteractionRepo = collectionArticlesDiscussionInteractionRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionArticlesDiscussionInteractionRequest"/>, GetDetail the <see cref="Entities.CollectionArticlesDiscussionInteraction"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesDiscussionInteraction"/></returns>
        public async Task<Result<CollectionArticlesDiscussionInteractionResponse>> Handle(GetDetailCollectionArticlesDiscussionInteractionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionArticlesDiscussionInteractionValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussionInteraction (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussionInteraction = collectionArticlesDiscussionInteractionRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionArticlesDiscussionInteraction == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussionInteraction), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INTERACTION_ID_NOT_FOUND);
            }
            return Result<CollectionArticlesDiscussionInteractionResponse>.Ok(CollectionArticlesDiscussionInteractionResponse.FromEntity(collectionArticlesDiscussionInteraction));
        }
    }
}
