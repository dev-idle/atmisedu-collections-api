using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.HUBS;

namespace atMISEDU.Application.UserCases.HUBS.HubsChannel
{
    /// <summary>
    /// Handler for <see cref="GetAllHubsChannelRequest"/>/ 
    /// </summary>
    public class GetAllHubsChannelHandler : IRequestHandler<GetAllHubsChannelRequest, Result<IQueryable<Entities.HubsChannel>>>
    {
        // Repository handle data access of <see cref="Entities.HubsChannel"/>>
        private readonly IHubsChannelSqlRepository hubsChannelRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllHubsChannelHandler"/>, inject needed dependency
        /// </summary>
        public GetAllHubsChannelHandler(IHubsChannelSqlRepository hubsChannelRepo)
        {
            this.hubsChannelRepo = hubsChannelRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllHubsChannelRequest"/>, GetAll the <see cref="Entities.HubsChannel"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.HubsChannel"/></returns>
        public async Task<Result<IQueryable<Entities.HubsChannel>>> Handle(GetAllHubsChannelRequest request, CancellationToken cancellationToken)
        {
            // Find HubsChannel from database and return
            var data = hubsChannelRepo.FindAll().AsQueryable();
            return Result<IQueryable<Entities.HubsChannel>>.Ok(data);
        }
    }
}
