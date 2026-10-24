using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Validator for <see cref="CreateCollectionArticlesAwardRequest"/>
    /// </summary>
    public class CreateCollectionArticlesAwardValidator : Validator<CreateCollectionArticlesAwardRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesAwardValidator"/>, register validator rules for <see cref="CreateCollectionArticlesAwardRequest"/>
        /// </summary>
        public CreateCollectionArticlesAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_INVALID);
            RuleFor(x => x.ArticleId).NotNull().GreaterThan(0);
            RuleFor(x => x.AwardId).NotNull().GreaterThan(0);
            RuleFor(x => x.CreatedBy).NotNull().GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
