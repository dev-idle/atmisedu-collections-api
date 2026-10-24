using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesInteractionValidator : Validator<UpdateCollectionArticlesInteractionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesInteractionValidator"/>, register validator rules for <see cref="UpdateCollectionArticlesInteractionRequest"/>
        /// </summary>
        public UpdateCollectionArticlesInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INTERACTION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.IntnType).NotEmpty().MaxLength(CollectionArticlesInteractionConst.INTN_TYPE_MAX_LENGTH).MustBeOneOf(CollectionConst.INTN_TYPES);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
