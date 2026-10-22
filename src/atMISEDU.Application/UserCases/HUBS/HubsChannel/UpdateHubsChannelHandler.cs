using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Application.Validators.HUBS.HubsChannel;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.HUBS;

namespace atMISEDU.Application.UserCases.HUBS.HubsChannel
{
    /// <summary>
    /// Handler for <see cref="UpdateHubsChannelRequest"/> 
    /// </summary>
    public class UpdateHubsChannelHandler : IRequestHandler<UpdateHubsChannelRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.HubsChannel"/>>
        private readonly IHubsChannelSqlRepository hubsChannelRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="UpdateHubsChannelHandler"/>, inject needed dependency
        /// </summary>
        public UpdateHubsChannelHandler(IHubsChannelSqlRepository hubsChannelRepo, ISqlUnitOfWork unitOfWork)
        {
            this.hubsChannelRepo = hubsChannelRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="UpdateHubsChannelRequest"/>, Update the <see cref="Entities.HubsChannel"/> base on data <see cref="UpdateHubsChannelRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(UpdateHubsChannelRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new UpdateHubsChannelValidator();
            validator.ValidateAndThrow(request);

            // Find HubsChannel from database, throw NotFoundException when not found
            var hubsChannel = await hubsChannelRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (hubsChannel == null)
                CustomException.ThrowNotFoundException(typeof(Entities.HubsChannel), MsgCode.ERR_HUBS_HUBS_CHANNEL_ID_NOT_FOUND);
            request.MapTo(hubsChannel, true);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked HubsChannel as Update state
                hubsChannelRepo.Update(hubsChannel);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok();
            }
            catch (Exception)
            {
                // Rollback transaction if any exception happened, then throw exception
                transaction.Rollback();
                throw;
            }
        }
    }
}
