using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Request for Delete <see cref="CollectionTags"/>
    /// </summary>
    public class DeleteCollectionTagsRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
