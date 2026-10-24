using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionArticlesVoteRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesVoteValidator : Validator<UpdateCollectionArticlesVoteRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesVoteValidator"/>, register validator rules for <see cref="UpdateCollectionArticlesVoteRequest"/>
        /// </summary>
        public UpdateCollectionArticlesVoteValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_VOTE_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.VotesPoint).InRange(CollectionConst.VOTES_POINT_MIN, CollectionConst.VOTES_POINT_MAX);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
