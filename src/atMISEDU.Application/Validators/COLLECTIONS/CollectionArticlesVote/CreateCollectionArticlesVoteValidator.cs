using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Validator for <see cref="CreateCollectionArticlesVoteRequest"/>
    /// </summary>
    public class CreateCollectionArticlesVoteValidator : Validator<CreateCollectionArticlesVoteRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesVoteValidator"/>, register validator rules for <see cref="CreateCollectionArticlesVoteRequest"/>
        /// </summary>
        public CreateCollectionArticlesVoteValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_VOTE_INVALID);
            RuleFor(x => x.ArticleId).NotNull().GreaterThan(0);
            RuleFor(x => x.CreatedBy).NotNull().GreaterThan(0);
            RuleFor(x => x.VotesPoint).NotNull().InRange(CollectionConst.VOTES_POINT_MIN, CollectionConst.VOTES_POINT_MAX);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
