using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Validator for <see cref="CreateCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionValidator : Validator<CreateCollectionArticlesDiscussionRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesDiscussionValidator"/>, register validator rules for <see cref="CreateCollectionArticlesDiscussionRequest"/>
        /// </summary>
        public CreateCollectionArticlesDiscussionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INVALID);
            RuleFor(x => x.DiscussionPid).GreaterThan(0);
            RuleFor(x => x.ArticleId).NotNull().GreaterThan(0);
            RuleFor(x => x.CreatedBy).NotNull().GreaterThan(0);
            RuleFor(x => x.DisContent).MaxLength(CollectionArticlesDiscussionConst.DIS_CONTENT_MAX_LENGTH);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
