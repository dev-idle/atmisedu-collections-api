using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionInteractionValidator : Validator<DeleteCollectionArticlesDiscussionInteractionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesDiscussionInteractionValidator"/>, register validator rules for <see cref="DeleteCollectionArticlesDiscussionInteractionRequest"/>
        /// </summary>
        public DeleteCollectionArticlesDiscussionInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INTERACTION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
