using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesInteractionValidator : Validator<DeleteCollectionArticlesInteractionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesInteractionValidator"/>, register validator rules for <see cref="DeleteCollectionArticlesInteractionRequest"/>
        /// </summary>
        public DeleteCollectionArticlesInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INTERACTION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
