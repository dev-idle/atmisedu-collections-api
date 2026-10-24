using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionValidator : Validator<UpdateCollectionArticlesDiscussionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesDiscussionValidator"/>, register validator rules for <see cref="UpdateCollectionArticlesDiscussionRequest"/>
        /// </summary>
        public UpdateCollectionArticlesDiscussionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.DisContent).MaxLength(CollectionArticlesDiscussionConst.DIS_CONTENT_MAX_LENGTH);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
