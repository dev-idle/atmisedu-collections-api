using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.HUBS.HubsChannel
{
    /// <summary>
    /// Request for Update <see cref="HubsChannel"/>
    /// </summary>
    public class UpdateHubsChannelRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? CnName { get; set; }
        public string? CnUrl { get; set; }
        public string? CnIcon { get; set; }
        public string? CnStyle { get; set; }
        public int? CnOrder { get; set; }
        public int? IsActived { get; set; }
    }
}
