using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesConfiguration : IEntityTypeConfiguration<CollectionArticles>
    {
        public void Configure(EntityTypeBuilder<CollectionArticles> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionArticlesConst.FIELD_ID);

            builder.Property(x => x.ArticleTitle).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_TITLE);
            builder.Property(x => x.ArticleAbstract).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_ABSTRACT);
            builder.Property(x => x.ArticleContent).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_CONTENT);
            builder.Property(x => x.ArticleThumbnail).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_THUMBNAIL);
            builder.Property(x => x.ArticlePublishedDate).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_PUBLISHED_DATE);
            builder.Property(x => x.ArticleTimeReading).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_TIME_READING);
            builder.Property(x => x.ArticleViews).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_VIEWS);
            builder.Property(x => x.ArticleIPAddress).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_IP_ADDRESS);
            builder.Property(x => x.ArticleBrowser).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_BROWSER);
            builder.Property(x => x.ArticleOstype).HasColumnName(CollectionArticlesConst.FIELD_ARTICLE_OSTYPE);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionArticlesConst.FIELD_CREATED_BY);
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionArticlesConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionArticlesConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES);
        }
    }
}