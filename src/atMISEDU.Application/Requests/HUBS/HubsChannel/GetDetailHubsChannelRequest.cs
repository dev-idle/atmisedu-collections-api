using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.HUBS;

namespace atMISEDU.Application.Requests.HUBS.HubsChannel
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.HubsChannel"/>
    /// </summary>
    public class GetDetailHubsChannelRequest : IQuery<Entities.HubsChannel>
    {
        public int? Id { get; set; }
    }
}
