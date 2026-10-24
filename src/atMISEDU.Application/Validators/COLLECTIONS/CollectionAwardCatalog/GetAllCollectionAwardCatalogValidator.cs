using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionAwardCatalogRequest"/>
    /// </summary>
    public class GetAllCollectionAwardCatalogValidator : Validator<GetAllCollectionAwardCatalogRequest>
    {
        public GetAllCollectionAwardCatalogValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_INVALID);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
