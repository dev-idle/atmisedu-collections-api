using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Validator for <see cref="GetDetailCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionValidator : Validator<GetDetailCollectionArticlesDiscussionRequest>
    {
        public GetDetailCollectionArticlesDiscussionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
