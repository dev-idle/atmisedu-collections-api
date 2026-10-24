using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionArticlesVote"/>, field names are same as database columns
    /// </summary>
    public class CollectionArticlesVoteResponse
    {
        public int VotesId { get; set; }
        public int ArticleId { get; set; }
        public int CreatedBy { get; set; }
        public int VotesPoint { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }

        /// <summary>
        /// Create response from <see cref="Entities.CollectionArticlesVote"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionArticlesVoteResponse FromEntity(Entities.CollectionArticlesVote entity)
        {
            var response = entity.MapTo<CollectionArticlesVoteResponse>()!;
            response.VotesId = entity.Id;
            return response;
        }
    }
}
