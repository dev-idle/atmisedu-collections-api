using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesInteractionHandler : IRequestHandler<GetAllCollectionArticlesInteractionRequest, Result<List<CollectionArticlesInteractionResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesInteraction"/>>
        private readonly ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionArticlesInteractionHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionArticlesInteractionHandler(ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo)
        {
            this.collectionArticlesInteractionRepo = collectionArticlesInteractionRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionArticlesInteractionRequest"/>, GetAll the <see cref="Entities.CollectionArticlesInteraction"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesInteraction"/></returns>
        public async Task<Result<List<CollectionArticlesInteractionResponse>>> Handle(GetAllCollectionArticlesInteractionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionArticlesInteractionValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by articleId and isActived when they are sent
            var query = collectionArticlesInteractionRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.ArticleId.HasValue)
                query = query.Where(x => x.ArticleId == request.ArticleId.Value);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionArticlesInteraction from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionArticlesInteractionResponse.FromEntity).ToList();
            return Result<List<CollectionArticlesInteractionResponse>>.Ok(data);
        }
    }
}
