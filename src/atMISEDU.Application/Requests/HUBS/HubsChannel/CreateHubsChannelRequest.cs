using atMISEDU.Contract.Abstractions;
using atMISEDU.Contract.Enumerations;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.HUBS.HubsChannel
{
    /// <summary>
    /// Request for Create <see cref="HubsChannel"/>
    /// </summary>
    public class CreateHubsChannelRequest : ICommand
    {
        public string? CnName { get; set; }
        public string? CnUrl { get; set; }
        public string? CnIcon { get; set; }
        public string? CnStyle { get; set; }
        public int? CnOrder { get; set; }
        [JsonIgnore]
        public int? IsActived { get; set; } = (int)ActivedType.ACTIVED;
    }
}
