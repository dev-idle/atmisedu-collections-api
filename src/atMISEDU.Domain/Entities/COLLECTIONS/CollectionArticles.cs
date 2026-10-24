using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    public class CollectionArticles : AggregateRoot<int>
    {
        public string ArticleTitle { get; set; }
        public string? ArticleAbstract { get; set; }
        public string? ArticleContent { get; set; }
        public string? ArticleThumbnail { get; set; }
        public int ArticlePublishedDate { get; set; }
        public int ArticleTimeReading { get; set; }
        public int ArticleViews { get; set; }
        public string ArticleIPAddress { get; set; }
        public string ArticleBrowser { get; set; }
        public string? ArticleOstype { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }
    }
}