using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionArticlesAwardRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesAwardHandler : IRequestHandler<GetDetailCollectionArticlesAwardRequest, Result<CollectionArticlesAwardResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesAward"/>>
        private readonly ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionArticlesAwardHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionArticlesAwardHandler(ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo)
        {
            this.collectionArticlesAwardRepo = collectionArticlesAwardRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionArticlesAwardRequest"/>, GetDetail the <see cref="Entities.CollectionArticlesAward"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesAward"/></returns>
        public async Task<Result<CollectionArticlesAwardResponse>> Handle(GetDetailCollectionArticlesAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionArticlesAwardValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesAward (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesAward = collectionArticlesAwardRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionArticlesAward == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesAward), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_ID_NOT_FOUND);
            }
            return Result<CollectionArticlesAwardResponse>.Ok(CollectionArticlesAwardResponse.FromEntity(collectionArticlesAward));
        }
    }
}
