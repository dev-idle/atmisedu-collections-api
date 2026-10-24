using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionAwardValidator : Validator<DeleteCollectionArticlesDiscussionAwardRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesDiscussionAwardValidator"/>, register validator rules for <see cref="DeleteCollectionArticlesDiscussionAwardRequest"/>
        /// </summary>
        public DeleteCollectionArticlesDiscussionAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_AWARD_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
