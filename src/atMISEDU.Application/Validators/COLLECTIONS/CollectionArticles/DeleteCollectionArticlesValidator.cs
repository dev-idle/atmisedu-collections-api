using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionArticlesRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesValidator : Validator<DeleteCollectionArticlesRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesValidator"/>, register validator rules for <see cref="DeleteCollectionArticlesRequest"/>
        /// </summary>
        public DeleteCollectionArticlesValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
