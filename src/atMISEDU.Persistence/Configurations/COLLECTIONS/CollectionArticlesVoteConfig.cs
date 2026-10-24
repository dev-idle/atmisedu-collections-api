using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.COLLECTIONS
{
    public class CollectionArticlesVoteConfiguration : IEntityTypeConfiguration<CollectionArticlesVote>
    {
        public void Configure(EntityTypeBuilder<CollectionArticlesVote> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CollectionArticlesVoteConst.FIELD_ID);

            builder.Property(x => x.ArticleId).HasColumnName(CollectionArticlesVoteConst.FIELD_ARTICLE_ID);
            builder.Property(x => x.CreatedBy).HasColumnName(CollectionArticlesVoteConst.FIELD_CREATED_BY);
            // Column votesPoint is tinyint in database, convert to int of entity
            builder.Property(x => x.VotesPoint).HasColumnName(CollectionArticlesVoteConst.FIELD_VOTES_POINT).HasConversion<byte>();
            builder.Property(x => x.CreatedDate).HasColumnName(CollectionArticlesVoteConst.FIELD_CREATED_DATE);
            // Column isActived is tinyint in database, convert to int of entity
            builder.Property(x => x.IsActived).HasColumnName(CollectionArticlesVoteConst.FIELD_IS_ACTIVED).HasConversion<byte>();

            builder.ToTable(TableConst.TABLE_COLLECTION_ARTICLES_VOTE);
        }
    }
}