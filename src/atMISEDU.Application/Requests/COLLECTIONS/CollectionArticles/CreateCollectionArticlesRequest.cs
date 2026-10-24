using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Request for Create <see cref="CollectionArticles"/>
    /// </summary>
    public class CreateCollectionArticlesRequest : ICommand
    {
        public string? ArticleTitle { get; set; }
        public string? ArticleAbstract { get; set; }
        public string? ArticleContent { get; set; }
        public string? ArticleThumbnail { get; set; }
        public int? ArticlePublishedDate { get; set; }
        public int? ArticleTimeReading { get; set; }
        public int? ArticleViews { get; set; }
        public string? ArticleIPAddress { get; set; }
        public string? ArticleBrowser { get; set; }
        public string? ArticleOstype { get; set; }
        public int? CreatedBy { get; set; }
        public int? IsActived { get; set; }
        // Ids (keyTags) of tags linked to the article
        public List<int>? TagIds { get; set; }
    }
}
