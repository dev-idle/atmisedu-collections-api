using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesDiscussionInteractionValidator : Validator<GetAllCollectionArticlesDiscussionInteractionRequest>
    {
        public GetAllCollectionArticlesDiscussionInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INTERACTION_INVALID);
            RuleFor(x => x.DiscussionId).GreaterThan(0);
            RuleFor(x => x.ArticleId).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
