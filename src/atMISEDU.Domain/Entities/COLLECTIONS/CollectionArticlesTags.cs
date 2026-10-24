using atMISEDU.Domain.Abstractions.Entities;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    /// <summary>
    /// Link between article and tag, primary key is (ArticleId, KeyTags)
    /// </summary>
    public class CollectionArticlesTags : IEntity
    {
        public int ArticleId { get; set; }
        public int KeyTags { get; set; }
    }
}
