using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.HUBS;

namespace atMISEDU.Application.Requests.HUBS.HubsChannel
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.HubsChannel"/>
    /// </summary>
    public class GetAllHubsChannelRequest : IQuery<IQueryable<Entities.HubsChannel>>
    {
    }
}
