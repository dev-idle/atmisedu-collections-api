using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Application.Validators.HUBS.HubsChannel;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.HUBS;

namespace atMISEDU.Application.UserCases.HUBS.HubsChannel
{
    /// <summary>
    /// Handler for <see cref="GetDetailHubsChannelRequest"/>/ 
    /// </summary>
    public class GetDetailHubsChannelHandler : IRequestHandler<GetDetailHubsChannelRequest, Result<Entities.HubsChannel>>
    {
        // Repository handle data access of <see cref="Entities.HubsChannel"/>>
        private readonly IHubsChannelSqlRepository hubsChannelRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailHubsChannelHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailHubsChannelHandler(IHubsChannelSqlRepository hubsChannelRepo)
        {
            this.hubsChannelRepo = hubsChannelRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailHubsChannelRequest"/>, GetDetail the <see cref="Entities.HubsChannel"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.HubsChannel"/></returns>
        public async Task<Result<Entities.HubsChannel>> Handle(GetDetailHubsChannelRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailHubsChannelValidator();
            validator.ValidateAndThrow(request);

            // Find HubsChannel from database, throw NotFoundException when not found
            var hubsChannel = hubsChannelRepo.FindAll(x => x.Id.Equals(request.Id)).FirstOrDefault();
            if (hubsChannel == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.HubsChannel), MsgCode.ERR_HUBS_HUBS_CHANNEL_ID_NOT_FOUND);
            }
            return hubsChannel;
        }
    }
}
