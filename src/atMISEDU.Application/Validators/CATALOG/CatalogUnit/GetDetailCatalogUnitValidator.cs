using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.CATALOG.CatalogUnit
{
    /// <summary>
    /// Validator for <see cref="GetDetailCatalogUnitRequest"/>
    /// </summary>
    public class GetDetailCatalogUnitValidator : Validator<GetDetailCatalogUnitRequest>
    {
         public GetDetailCatalogUnitValidator()
          {
               WithValidator(MsgCode.ERR_CATALOG_CATALOG_UNIT_INVALID);
               RuleFor(x => x.Id).NotNull().GreaterThan(0);
          }
    }
}
