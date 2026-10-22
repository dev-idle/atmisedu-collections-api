using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Entities.HUBS;

namespace atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS
{
    /// <summary>
    /// Provide repository for <see cref="HubsChannel"/>, inherit from <see cref="IGenericSqlRepository{TEntity,TKey}"/>
    /// </summary>
    public interface IHubsChannelSqlRepository : IGenericSqlRepository<HubsChannel, int>
    {
    }
}
