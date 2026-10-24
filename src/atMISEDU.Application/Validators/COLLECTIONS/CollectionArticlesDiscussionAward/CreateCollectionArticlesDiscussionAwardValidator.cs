using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Validator for <see cref="CreateCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionAwardValidator : Validator<CreateCollectionArticlesDiscussionAwardRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesDiscussionAwardValidator"/>, register validator rules for <see cref="CreateCollectionArticlesDiscussionAwardRequest"/>
        /// </summary>
        public CreateCollectionArticlesDiscussionAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_AWARD_INVALID);
            RuleFor(x => x.DiscussionId).NotNull().GreaterThan(0);
            RuleFor(x => x.AwardId).NotNull().GreaterThan(0);
            RuleFor(x => x.CreatedBy).NotNull().GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
