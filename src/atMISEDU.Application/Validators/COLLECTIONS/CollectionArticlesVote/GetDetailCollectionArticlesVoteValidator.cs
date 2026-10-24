using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Validator for <see cref="GetDetailCollectionArticlesVoteRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesVoteValidator : Validator<GetDetailCollectionArticlesVoteRequest>
    {
        public GetDetailCollectionArticlesVoteValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_VOTE_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
