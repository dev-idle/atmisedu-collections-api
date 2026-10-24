using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Request for Update <see cref="CollectionArticles"/>
    /// </summary>
    public class UpdateCollectionArticlesRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
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
        public int? IsActived { get; set; }
        // Ids (keyTags) of tags linked to the article. Null: keep links, empty: remove all links, list: replace links
        public List<int>? TagIds { get; set; }
    }
}
