using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionAwardHandler : IRequestHandler<GetDetailCollectionArticlesDiscussionAwardRequest, Result<CollectionArticlesDiscussionAwardResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionAward"/>>
        private readonly ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionArticlesDiscussionAwardHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionArticlesDiscussionAwardHandler(ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo)
        {
            this.collectionArticlesDiscussionAwardRepo = collectionArticlesDiscussionAwardRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionArticlesDiscussionAwardRequest"/>, GetDetail the <see cref="Entities.CollectionArticlesDiscussionAward"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesDiscussionAward"/></returns>
        public async Task<Result<CollectionArticlesDiscussionAwardResponse>> Handle(GetDetailCollectionArticlesDiscussionAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionArticlesDiscussionAwardValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussionAward (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussionAward = collectionArticlesDiscussionAwardRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionArticlesDiscussionAward == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussionAward), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_AWARD_ID_NOT_FOUND);
            }
            return Result<CollectionArticlesDiscussionAwardResponse>.Ok(CollectionArticlesDiscussionAwardResponse.FromEntity(collectionArticlesDiscussionAward));
        }
    }
}
