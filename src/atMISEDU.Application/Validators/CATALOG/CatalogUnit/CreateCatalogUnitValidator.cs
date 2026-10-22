using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.CATALOG;

namespace atMISEDU.Application.Validators.CATALOG.CatalogUnit
{
    /// <summary>
    /// Validator for <see cref="CreateCatalogUnitRequest"/>
    /// </summary>
    public class CreateCatalogUnitValidator : Validator<CreateCatalogUnitRequest>
    {
    /// <summary>
    /// Constructor of <see cref="CreateCatalogUnitValidator"/>, register validator rules for <see cref="CreateCatalogUnitRequest"/>
    /// </summary>
         public CreateCatalogUnitValidator()
          {
               WithValidator(MsgCode.ERR_CATALOG_CATALOG_UNIT_INVALID);
               RuleFor(x => x.CugID).NotNull().GreaterThan(0);
               RuleFor(x => x.UnitCode).NotNull().MaxLength(CatalogUnitConst.UNIT_CODE_MAX_LENGTH);
               RuleFor(x => x.UnitName).NotNull().MaxLength(CatalogUnitConst.UNIT_NAME_MAX_LENGTH);
               RuleFor(x => x.UnitEnglish).NotNull().MaxLength(CatalogUnitConst.UNIT_ENGLISH_MAX_LENGTH);
          }
    }
}
