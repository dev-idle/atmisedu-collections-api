using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionInteractionValidator : Validator<UpdateCollectionArticlesDiscussionInteractionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesDiscussionInteractionValidator"/>, register validator rules for <see cref="UpdateCollectionArticlesDiscussionInteractionRequest"/>
        /// </summary>
        public UpdateCollectionArticlesDiscussionInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INTERACTION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.IntnType).NotEmpty().MaxLength(CollectionArticlesDiscussionInteractionConst.INTN_TYPE_MAX_LENGTH).MustBeOneOf(CollectionConst.INTN_TYPES);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
