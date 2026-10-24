using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionArticlesAwardRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesAwardHandler : IRequestHandler<GetAllCollectionArticlesAwardRequest, Result<List<CollectionArticlesAwardResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesAward"/>>
        private readonly ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionArticlesAwardHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionArticlesAwardHandler(ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo)
        {
            this.collectionArticlesAwardRepo = collectionArticlesAwardRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionArticlesAwardRequest"/>, GetAll the <see cref="Entities.CollectionArticlesAward"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesAward"/></returns>
        public async Task<Result<List<CollectionArticlesAwardResponse>>> Handle(GetAllCollectionArticlesAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionArticlesAwardValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by articleId and isActived when they are sent (combined with AND)
            var query = collectionArticlesAwardRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.ArticleId.HasValue)
                query = query.Where(x => x.ArticleId == request.ArticleId.Value);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionArticlesAward from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionArticlesAwardResponse.FromEntity).ToList();
            return Result<List<CollectionArticlesAwardResponse>>.Ok(data);
        }
    }
}
