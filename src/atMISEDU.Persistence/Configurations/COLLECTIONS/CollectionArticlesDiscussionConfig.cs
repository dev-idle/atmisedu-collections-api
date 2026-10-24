using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesDiscussionConfiguration : IEntityTypeConfiguration<CollectionArticlesDiscussion>
    {
        public void Configure(EntityTypeBuilder<CollectionArticlesDiscussion> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionArticlesDiscussionConst.FIELD_ID);

            builder.Property(x => x.DiscussionPid).HasColumnName(CollectionArticlesDiscussionConst.FIELD_DISCUSSION_PID);
            builder.Property(x => x.ArticleId).HasColumnName(CollectionArticlesDiscussionConst.FIELD_ARTICLE_ID);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionArticlesDiscussionConst.FIELD_CREATED_BY);
            builder.Property(x => x.DisContent).HasColumnName(CollectionArticlesDiscussionConst.FIELD_DIS_CONTENT);
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionArticlesDiscussionConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionArticlesDiscussionConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES_DISCUSSION);
        }
    }
}