using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionArticlesVoteRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesVoteHandler : IRequestHandler<GetDetailCollectionArticlesVoteRequest, Result<CollectionArticlesVoteResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesVote"/>>
        private readonly ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionArticlesVoteHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionArticlesVoteHandler(ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo)
        {
            this.collectionArticlesVoteRepo = collectionArticlesVoteRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionArticlesVoteRequest"/>, GetDetail the <see cref="Entities.CollectionArticlesVote"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesVote"/></returns>
        public async Task<Result<CollectionArticlesVoteResponse>> Handle(GetDetailCollectionArticlesVoteRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionArticlesVoteValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesVote (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesVote = collectionArticlesVoteRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionArticlesVote == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesVote), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_VOTE_ID_NOT_FOUND);
            }
            return Result<CollectionArticlesVoteResponse>.Ok(CollectionArticlesVoteResponse.FromEntity(collectionArticlesVote));
        }
    }
}
