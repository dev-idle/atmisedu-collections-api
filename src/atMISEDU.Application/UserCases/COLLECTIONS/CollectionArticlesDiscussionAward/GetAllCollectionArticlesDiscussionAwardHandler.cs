using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesDiscussionAwardHandler : IRequestHandler<GetAllCollectionArticlesDiscussionAwardRequest, Result<List<CollectionArticlesDiscussionAwardResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionAward"/>>
        private readonly ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionArticlesDiscussionAwardHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionArticlesDiscussionAwardHandler(ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo)
        {
            this.collectionArticlesDiscussionAwardRepo = collectionArticlesDiscussionAwardRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionArticlesDiscussionAwardRequest"/>, GetAll the <see cref="Entities.CollectionArticlesDiscussionAward"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesDiscussionAward"/></returns>
        public async Task<Result<List<CollectionArticlesDiscussionAwardResponse>>> Handle(GetAllCollectionArticlesDiscussionAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionArticlesDiscussionAwardValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by discussionId and isActived when they are sent (combined with AND)
            var query = collectionArticlesDiscussionAwardRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.DiscussionId.HasValue)
                query = query.Where(x => x.DiscussionId == request.DiscussionId.Value);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionArticlesDiscussionAward from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionArticlesDiscussionAwardResponse.FromEntity).ToList();
            return Result<List<CollectionArticlesDiscussionAwardResponse>>.Ok(data);
        }
    }
}
