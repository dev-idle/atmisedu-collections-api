using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Validator for <see cref="CreateCollectionAwardCatalogRequest"/>
    /// </summary>
    public class CreateCollectionAwardCatalogValidator : Validator<CreateCollectionAwardCatalogRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateCollectionAwardCatalogValidator"/>, register validator rules for <see cref="CreateCollectionAwardCatalogRequest"/>
        /// </summary>
        public CreateCollectionAwardCatalogValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_INVALID);
            RuleFor(x => x.AwIcon).NotNull().NotEmpty().MaxLength(CollectionAwardCatalogConst.AW_ICON_MAX_LENGTH);
            RuleFor(x => x.AwPoint).GreaterThanOrEqual(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
