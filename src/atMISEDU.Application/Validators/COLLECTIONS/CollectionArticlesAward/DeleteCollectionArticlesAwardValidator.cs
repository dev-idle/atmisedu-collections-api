using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionArticlesAwardRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesAwardValidator : Validator<DeleteCollectionArticlesAwardRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesAwardValidator"/>, register validator rules for <see cref="DeleteCollectionArticlesAwardRequest"/>
        /// </summary>
        public DeleteCollectionArticlesAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
