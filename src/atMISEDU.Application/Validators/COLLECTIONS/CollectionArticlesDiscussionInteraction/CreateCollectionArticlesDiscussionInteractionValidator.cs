using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Validator for <see cref="CreateCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionInteractionValidator : Validator<CreateCollectionArticlesDiscussionInteractionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesDiscussionInteractionValidator"/>, register validator rules for <see cref="CreateCollectionArticlesDiscussionInteractionRequest"/>
        /// </summary>
        public CreateCollectionArticlesDiscussionInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INTERACTION_INVALID);
            RuleFor(x => x.DiscussionId).NotNull().GreaterThan(0);
            RuleFor(x => x.CreatedBy).NotNull().GreaterThan(0);
            RuleFor(x => x.IntnType).NotNull().NotEmpty().MaxLength(CollectionArticlesDiscussionInteractionConst.INTN_TYPE_MAX_LENGTH).MustBeOneOf(CollectionConst.INTN_TYPES);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
