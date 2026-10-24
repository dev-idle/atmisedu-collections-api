using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Validator for <see cref="GetDetailCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionInteractionValidator : Validator<GetDetailCollectionArticlesDiscussionInteractionRequest>
    {
        public GetDetailCollectionArticlesDiscussionInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INTERACTION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
