using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionAwardCatalogRequest"/>
    /// </summary>
    public class UpdateCollectionAwardCatalogValidator : Validator<UpdateCollectionAwardCatalogRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionAwardCatalogValidator"/>, register validator rules for <see cref="UpdateCollectionAwardCatalogRequest"/>
        /// </summary>
        public UpdateCollectionAwardCatalogValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.AwIcon).NotEmpty().MaxLength(CollectionAwardCatalogConst.AW_ICON_MAX_LENGTH);
            RuleFor(x => x.AwPoint).GreaterThanOrEqual(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
