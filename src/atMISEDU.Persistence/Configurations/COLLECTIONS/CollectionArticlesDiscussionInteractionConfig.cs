using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesDiscussionInteractionConfiguration : IEntityTypeConfiguration<CollectionArticlesDiscussionInteraction>
    {
        public void Configure(EntityTypeBuilder<CollectionArticlesDiscussionInteraction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionArticlesDiscussionInteractionConst.FIELD_ID);

            builder.Property(x => x.DiscussionId).HasColumnName(CollectionArticlesDiscussionInteractionConst.FIELD_DISCUSSION_ID);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionArticlesDiscussionInteractionConst.FIELD_CREATED_BY);
            builder.Property(x => x.IntnType).HasColumnName(CollectionArticlesDiscussionInteractionConst.FIELD_INTN_TYPE);
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionArticlesDiscussionInteractionConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionArticlesDiscussionInteractionConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES_DISCUSSION_INTERACTION);
        }
    }
}