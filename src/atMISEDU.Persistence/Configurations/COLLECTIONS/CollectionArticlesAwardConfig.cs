using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesAwardConfiguration : IEntityTypeConfiguration<CollectionArticlesAward>
    {
        public void Configure(EntityTypeBuilder<CollectionArticlesAward> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionArticlesAwardConst.FIELD_ID);

            builder.Property(x => x.ArticleId).HasColumnName(CollectionArticlesAwardConst.FIELD_ARTICLE_ID);
            builder.Property(x => x.AwardId).HasColumnName(CollectionArticlesAwardConst.FIELD_AWARD_ID);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionArticlesAwardConst.FIELD_CREATED_BY);
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionArticlesAwardConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionArticlesAwardConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES_AWARD);
        }
    }
}