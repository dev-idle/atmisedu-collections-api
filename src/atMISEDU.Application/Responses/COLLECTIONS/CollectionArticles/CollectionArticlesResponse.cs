using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionArticles"/>, field names are same as database columns
    /// </summary>
    public class CollectionArticlesResponse
    {
        public int ArticleId { get; set; }
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

        /// <summary>
        /// Create response from <see cref="Entities.CollectionArticles"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionArticlesResponse FromEntity(Entities.CollectionArticles entity)
        {
            var response = entity.MapTo<CollectionArticlesResponse>()!;
            response.ArticleId = entity.Id;
            return response;
        }
    }
}
