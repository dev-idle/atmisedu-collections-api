using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Handler for <see cref="CreateCollectionArticlesVoteRequest"/>
    /// </summary>
    public class CreateCollectionArticlesVoteHandler : IRequestHandler<CreateCollectionArticlesVoteRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesVote"/>>
        private readonly ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesVoteHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionArticlesVoteHandler(ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo, ICollectionArticlesSqlRepository collectionArticlesRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesVoteRepo = collectionArticlesVoteRepo;
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionArticlesVoteRequest"/>, Create the <see cref="Entities.CollectionArticlesVote"/> base on data <see cref="CreateCollectionArticlesVoteRequest"/>
        /// and save to database. If same user already voted same article (include deleted record), reuse that record instead of creating new one
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created (or reused) <see cref="Entities.CollectionArticlesVote"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionArticlesVoteRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new CreateCollectionArticlesVoteValidator();
            validator.ValidateAndThrow(request);

            // Check CollectionArticles (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionArticlesRepo.IsExistAsync(x => x.Id == request.ArticleId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticles), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_ID_NOT_FOUND);

            // Find CollectionArticlesVote of same user on same article (include deleted record) to reuse
            var collectionArticlesVote = await collectionArticlesVoteRepo.FindSingleAsync(x => x.ArticleId == request.ArticleId && x.CreatedBy == request.CreatedBy, true, cancellationToken);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                if (collectionArticlesVote != null)
                {
                    // Same user post again, update votes point and status, keep id and createdDate
                    collectionArticlesVote.VotesPoint = (int)request.VotesPoint;
                    collectionArticlesVote.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;
                    // Marked CollectionArticlesVote as Update state
                    collectionArticlesVoteRepo.Update(collectionArticlesVote);
                }
                else
                {
                    // Create new CollectionArticlesVote from request, apply default values
                    collectionArticlesVote = request.MapTo<Entities.CollectionArticlesVote>()!;
                    collectionArticlesVote.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    collectionArticlesVote.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;
                    // Marked CollectionArticlesVote as Create state
                    collectionArticlesVoteRepo.Add(collectionArticlesVote);
                }
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { votesId = collectionArticlesVote.Id });
            }
            catch (Exception)
            {
                // Rollback transaction if any exception happened, then throw exception
                transaction.Rollback();
                throw;
            }
        }
    }
}
