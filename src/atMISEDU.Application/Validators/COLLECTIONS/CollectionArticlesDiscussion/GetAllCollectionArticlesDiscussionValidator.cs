using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesDiscussionValidator : Validator<GetAllCollectionArticlesDiscussionRequest>
    {
        public GetAllCollectionArticlesDiscussionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INVALID);
            RuleFor(x => x.ArticleId).GreaterThan(0);
            RuleFor(x => x.DiscussionPid).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
