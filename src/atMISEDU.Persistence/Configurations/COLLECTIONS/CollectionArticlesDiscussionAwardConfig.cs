using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesDiscussionAwardConfiguration : IEntityTypeConfiguration<CollectionArticlesDiscussionAward>
    {
        public void Configure(EntityTypeBuilder<CollectionArticlesDiscussionAward> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionArticlesDiscussionAwardConst.FIELD_ID);

            builder.Property(x => x.DiscussionId).HasColumnName(CollectionArticlesDiscussionAwardConst.FIELD_DISCUSSION_ID);
            builder.Property(x => x.AwardId).HasColumnName(CollectionArticlesDiscussionAwardConst.FIELD_AWARD_ID);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionArticlesDiscussionAwardConst.FIELD_CREATED_BY);
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionArticlesDiscussionAwardConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionArticlesDiscussionAwardConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES_DISCUSSION_AWARD);
        }
    }
}