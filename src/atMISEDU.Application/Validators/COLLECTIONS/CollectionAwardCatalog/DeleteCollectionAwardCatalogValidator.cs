using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionAwardCatalogRequest"/>
    /// </summary>
    public class DeleteCollectionAwardCatalogValidator : Validator<DeleteCollectionAwardCatalogRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionAwardCatalogValidator"/>, register validator rules for <see cref="DeleteCollectionAwardCatalogRequest"/>
        /// </summary>
        public DeleteCollectionAwardCatalogValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
