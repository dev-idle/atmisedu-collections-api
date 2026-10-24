using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesDiscussionAwardValidator : Validator<GetAllCollectionArticlesDiscussionAwardRequest>
    {
        public GetAllCollectionArticlesDiscussionAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_AWARD_INVALID);
            RuleFor(x => x.DiscussionId).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
