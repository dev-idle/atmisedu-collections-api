using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.CATALOG;

namespace atMISEDU.Application.Validators.CATALOG.CatalogUnit
{
    /// <summary>
    /// Validator for <see cref="UpdateCatalogUnitRequest"/>
    /// </summary>
    public class UpdateCatalogUnitValidator : Validator<UpdateCatalogUnitRequest>
    {
    /// <summary>
    /// Constructor of <see cref="UpdateCatalogUnitValidator"/>, register validator rules for <see cref="UpdateCatalogUnitRequest"/>
    /// </summary>
         public UpdateCatalogUnitValidator()
          {
               WithValidator(MsgCode.ERR_CATALOG_CATALOG_UNIT_INVALID);
               RuleFor(x => x.Id).NotNull().GreaterThan(0);
               RuleFor(x => x.CugID).GreaterThan(0);
               RuleFor(x => x.UnitCode).MaxLength(CatalogUnitConst.UNIT_CODE_MAX_LENGTH);
               RuleFor(x => x.UnitName).MaxLength(CatalogUnitConst.UNIT_NAME_MAX_LENGTH);
               RuleFor(x => x.UnitEnglish).MaxLength(CatalogUnitConst.UNIT_ENGLISH_MAX_LENGTH);
          }
    }
}
