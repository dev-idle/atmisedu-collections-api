using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Request for Update <see cref="CollectionArticlesAward"/>
    /// </summary>
    public class UpdateCollectionArticlesAwardRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? AwardId { get; set; }
        public int? IsActived { get; set; }
    }
}
