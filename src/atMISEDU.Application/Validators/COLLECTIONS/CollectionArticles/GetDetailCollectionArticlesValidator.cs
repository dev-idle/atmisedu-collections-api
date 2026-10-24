using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Validator for <see cref="GetDetailCollectionArticlesRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesValidator : Validator<GetDetailCollectionArticlesRequest>
    {
        public GetDetailCollectionArticlesValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
