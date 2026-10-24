using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesTagsConfiguration : IEntityTypeConfiguration<CollectionArticlesTags>
    {
        public void Configure(EntityTypeBuilder<CollectionArticlesTags> builder)
        {
            builder.HasKey(x => new { x.ArticleId, x.KeyTags });

            builder.Property(x => x.ArticleId).HasColumnName(CollectionArticlesTagsConst.FIELD_ARTICLE_ID);
            builder.Property(x => x.KeyTags).HasColumnName(CollectionArticlesTagsConst.FIELD_KEY_TAGS);

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES_TAGS);
        }
    }
}
