using atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS;
using atMISEDU.Domain.Entities.HUBS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.HUBS
{
    /// <summary>
    /// Implementation of IHubsChannelSqlRepository
    /// </summary>
    public class HubsChannelSqlRepository(ApplicationDbContext context) : GenericSqlRepository<HubsChannel, int>(context), IHubsChannelSqlRepository
    {
    }
}
