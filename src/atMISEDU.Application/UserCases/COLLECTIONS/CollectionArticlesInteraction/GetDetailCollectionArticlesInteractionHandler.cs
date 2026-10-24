using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesInteractionHandler : IRequestHandler<GetDetailCollectionArticlesInteractionRequest, Result<CollectionArticlesInteractionResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesInteraction"/>>
        private readonly ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionArticlesInteractionHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionArticlesInteractionHandler(ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo)
        {
            this.collectionArticlesInteractionRepo = collectionArticlesInteractionRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionArticlesInteractionRequest"/>, GetDetail the <see cref="Entities.CollectionArticlesInteraction"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesInteraction"/></returns>
        public async Task<Result<CollectionArticlesInteractionResponse>> Handle(GetDetailCollectionArticlesInteractionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionArticlesInteractionValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesInteraction (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesInteraction = collectionArticlesInteractionRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionArticlesInteraction == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesInteraction), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INTERACTION_ID_NOT_FOUND);
            }
            return Result<CollectionArticlesInteractionResponse>.Ok(CollectionArticlesInteractionResponse.FromEntity(collectionArticlesInteraction));
        }
    }
}
