using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionArticlesVoteRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesVoteHandler : IRequestHandler<GetAllCollectionArticlesVoteRequest, Result<List<CollectionArticlesVoteResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesVote"/>>
        private readonly ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionArticlesVoteHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionArticlesVoteHandler(ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo)
        {
            this.collectionArticlesVoteRepo = collectionArticlesVoteRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionArticlesVoteRequest"/>, GetAll the <see cref="Entities.CollectionArticlesVote"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesVote"/></returns>
        public async Task<Result<List<CollectionArticlesVoteResponse>>> Handle(GetAllCollectionArticlesVoteRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionArticlesVoteValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by articleId and isActived when they are sent
            var query = collectionArticlesVoteRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.ArticleId.HasValue)
                query = query.Where(x => x.ArticleId == request.ArticleId.Value);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionArticlesVote from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionArticlesVoteResponse.FromEntity).ToList();
            return Result<List<CollectionArticlesVoteResponse>>.Ok(data);
        }
    }
}
