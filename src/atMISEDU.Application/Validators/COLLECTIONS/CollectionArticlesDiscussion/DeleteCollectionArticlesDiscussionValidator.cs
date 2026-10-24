using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionValidator : Validator<DeleteCollectionArticlesDiscussionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesDiscussionValidator"/>, register validator rules for <see cref="DeleteCollectionArticlesDiscussionRequest"/>
        /// </summary>
        public DeleteCollectionArticlesDiscussionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
