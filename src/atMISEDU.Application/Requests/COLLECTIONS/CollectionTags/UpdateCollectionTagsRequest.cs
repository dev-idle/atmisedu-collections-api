using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Request for Update <see cref="CollectionTags"/>
    /// </summary>
    public class UpdateCollectionTagsRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? TagsName { get; set; }
        public string? TagsDescription { get; set; }
        public string? TagsIcon { get; set; }
        public string? TagsStyle { get; set; }
        public int? IsActived { get; set; }
    }
}
