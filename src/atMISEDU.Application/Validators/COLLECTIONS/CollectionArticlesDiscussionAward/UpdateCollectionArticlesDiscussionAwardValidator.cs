using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionAwardValidator : Validator<UpdateCollectionArticlesDiscussionAwardRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesDiscussionAwardValidator"/>, register validator rules for <see cref="UpdateCollectionArticlesDiscussionAwardRequest"/>
        /// </summary>
        public UpdateCollectionArticlesDiscussionAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_AWARD_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.AwardId).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
