using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesInteractionConfiguration : IEntityTypeConfiguration<CollectionArticlesInteraction>
    {
        public void Configure(EntityTypeBuilder<CollectionArticlesInteraction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionArticlesInteractionConst.FIELD_ID);

            builder.Property(x => x.ArticleId).HasColumnName(CollectionArticlesInteractionConst.FIELD_ARTICLE_ID);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionArticlesInteractionConst.FIELD_CREATED_BY);
            builder.Property(x => x.IntnType).HasColumnName(CollectionArticlesInteractionConst.FIELD_INTN_TYPE);
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionArticlesInteractionConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionArticlesInteractionConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES_INTERACTION);
        }
    }
}