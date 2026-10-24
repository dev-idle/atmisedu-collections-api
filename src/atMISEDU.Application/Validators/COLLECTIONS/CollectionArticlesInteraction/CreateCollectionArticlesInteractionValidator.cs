using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Validator for <see cref="CreateCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class CreateCollectionArticlesInteractionValidator : Validator<CreateCollectionArticlesInteractionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesInteractionValidator"/>, register validator rules for <see cref="CreateCollectionArticlesInteractionRequest"/>
        /// </summary>
        public CreateCollectionArticlesInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INTERACTION_INVALID);
            RuleFor(x => x.ArticleId).NotNull().GreaterThan(0);
            RuleFor(x => x.CreatedBy).NotNull().GreaterThan(0);
            RuleFor(x => x.IntnType).NotNull().NotEmpty().MaxLength(CollectionArticlesInteractionConst.INTN_TYPE_MAX_LENGTH).MustBeOneOf(CollectionConst.INTN_TYPES);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
