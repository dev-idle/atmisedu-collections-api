using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.CATALOG;
using atMISEDU.Domain.Entities.CATALOG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.CATALOG
{
    public class CatalogUnitConfiguration : IEntityTypeConfiguration<CatalogUnit>
    {
        public void Configure(EntityTypeBuilder<CatalogUnit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CatalogUnitConst.FIELD_ID);

            builder.Property(x => x.CugID).HasColumnName(CatalogUnitConst.FIELD_CUG_ID);
            builder.Property(x => x.UnitCode).HasColumnName(CatalogUnitConst.FIELD_UNIT_CODE);
            builder.Property(x => x.UnitName).HasColumnName(CatalogUnitConst.FIELD_UNIT_NAME);
            builder.Property(x => x.UnitEnglish).HasColumnName(CatalogUnitConst.FIELD_UNIT_ENGLISH);

            builder.ToTable(TableConst.TABLE_CATALOG_UNIT);
        }
    }
}
