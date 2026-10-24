using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionAwardCatalogConfiguration : IEntityTypeConfiguration<CollectionAwardCatalog>
    {
        public void Configure(EntityTypeBuilder<CollectionAwardCatalog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionAwardCatalogConst.FIELD_ID);

            builder.Property(x => x.AwIcon).HasColumnName(CollectionAwardCatalogConst.FIELD_AW_ICON);
            builder.Property(x => x.AwPoint).HasColumnName(CollectionAwardCatalogConst.FIELD_AW_POINT);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionAwardCatalogConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_AWARD_CATALOG);
        }
    }
}