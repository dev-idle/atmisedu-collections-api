using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Validator for <see cref="GetDetailCollectionAwardCatalogRequest"/>
    /// </summary>
    public class GetDetailCollectionAwardCatalogValidator : Validator<GetDetailCollectionAwardCatalogRequest>
    {
        public GetDetailCollectionAwardCatalogValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
