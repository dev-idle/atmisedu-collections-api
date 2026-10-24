using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionHandler : IRequestHandler<GetDetailCollectionArticlesDiscussionRequest, Result<CollectionArticlesDiscussionDetailResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionInteraction"/>>
        private readonly ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionAward"/>>
        private readonly ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionArticlesDiscussionHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionArticlesDiscussionHandler(ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo,
                                                            ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo,
                                                            ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo)
        {
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
            this.collectionArticlesDiscussionInteractionRepo = collectionArticlesDiscussionInteractionRepo;
            this.collectionArticlesDiscussionAwardRepo = collectionArticlesDiscussionAwardRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionArticlesDiscussionRequest"/>, GetDetail the <see cref="Entities.CollectionArticlesDiscussion"/> from database,
        /// include count of actived interactions and awards of this discussion (replies are not counted)
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesDiscussion"/></returns>
        public async Task<Result<CollectionArticlesDiscussionDetailResponse>> Handle(GetDetailCollectionArticlesDiscussionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionArticlesDiscussionValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussion (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussion = collectionArticlesDiscussionRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionArticlesDiscussion == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussion), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_ID_NOT_FOUND);
            }

            // Count actived interactions and actived awards of this CollectionArticlesDiscussion
            var interactionCount = collectionArticlesDiscussionInteractionRepo.FindAll(x => x.DiscussionId == request.Id && x.IsActived == (int)ActivedType.ACTIVED).Count();
            var awardCount = collectionArticlesDiscussionAwardRepo.FindAll(x => x.DiscussionId == request.Id && x.IsActived == (int)ActivedType.ACTIVED).Count();

            return Result<CollectionArticlesDiscussionDetailResponse>.Ok(CollectionArticlesDiscussionDetailResponse.FromEntity(collectionArticlesDiscussion, interactionCount, awardCount));
        }
    }
}
