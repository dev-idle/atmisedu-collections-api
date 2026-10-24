using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticles;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionArticlesRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesHandler : IRequestHandler<GetAllCollectionArticlesRequest, Result<List<CollectionArticlesResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesTags"/>>
        private readonly ICollectionArticlesTagsSqlRepository collectionArticlesTagsRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionArticlesHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionArticlesHandler(ICollectionArticlesSqlRepository collectionArticlesRepo, ICollectionArticlesTagsSqlRepository collectionArticlesTagsRepo)
        {
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.collectionArticlesTagsRepo = collectionArticlesTagsRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionArticlesRequest"/>, GetAll the <see cref="Entities.CollectionArticles"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticles"/></returns>
        public async Task<Result<List<CollectionArticlesResponse>>> Handle(GetAllCollectionArticlesRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionArticlesValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by isActived when it is sent
            var query = collectionArticlesRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);
            // Filter articles which have link with keyTags when it is sent
            if (request.KeyTags.HasValue)
            {
                var articleIds = collectionArticlesTagsRepo.FindAll(x => x.KeyTags == request.KeyTags.Value).Select(x => x.ArticleId);
                query = query.Where(x => articleIds.Contains(x.Id));
            }

            // Find CollectionArticles from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionArticlesResponse.FromEntity).ToList();
            return Result<List<CollectionArticlesResponse>>.Ok(data);
        }
    }
}
