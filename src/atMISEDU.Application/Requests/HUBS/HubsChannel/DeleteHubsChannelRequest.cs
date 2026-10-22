using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.HUBS.HubsChannel
{
    /// <summary>
    /// Request for Delete <see cref="HubsChannel"/>
    /// </summary>
    public class DeleteHubsChannelRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
