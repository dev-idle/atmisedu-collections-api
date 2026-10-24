using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionArticlesVoteRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesVoteValidator : Validator<GetAllCollectionArticlesVoteRequest>
    {
        public GetAllCollectionArticlesVoteValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_VOTE_INVALID);
            RuleFor(x => x.ArticleId).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
