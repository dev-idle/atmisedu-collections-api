using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Request for Create <see cref="CollectionTags"/>
    /// </summary>
    public class CreateCollectionTagsRequest : ICommand
    {
        public string? TagsName { get; set; }
        public string? TagsDescription { get; set; }
        public string? TagsIcon { get; set; }
        public string? TagsStyle { get; set; }
        public int? CreatedBy { get; set; }
        public int? IsActived { get; set; }
    }
}
