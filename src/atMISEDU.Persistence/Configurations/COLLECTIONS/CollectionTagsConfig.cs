using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionTagsConfiguration : IEntityTypeConfiguration<CollectionTags>
    {
        public void Configure(EntityTypeBuilder<CollectionTags> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionTagsConst.FIELD_ID);

            builder.Property(x => x.TagsName).HasColumnName(CollectionTagsConst.FIELD_TAGS_NAME);
            builder.Property(x => x.TagsDescription).HasColumnName(CollectionTagsConst.FIELD_TAGS_DESCRIPTION);
            builder.Property(x => x.TagsIcon).HasColumnName(CollectionTagsConst.FIELD_TAGS_ICON);
            builder.Property(x => x.TagsStyle).HasColumnName(CollectionTagsConst.FIELD_TAGS_STYLE);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionTagsConst.FIELD_CREATED_BY);
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionTagsConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionTagsConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_TAGS);
        }
    }
}