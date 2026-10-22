using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Application.Validators.HUBS.HubsChannel;
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
    /// Handler for <see cref="DeleteHubsChannelRequest"/> 
    /// </summary>
    public class DeleteHubsChannelHandler : IRequestHandler<DeleteHubsChannelRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.HubsChannel"/>>
        private readonly IHubsChannelSqlRepository hubsChannelRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteHubsChannelHandler"/>, inject needed dependency
        /// </summary>
        public DeleteHubsChannelHandler(IHubsChannelSqlRepository hubsChannelRepo, ISqlUnitOfWork unitOfWork)
        {
            this.hubsChannelRepo = hubsChannelRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteHubsChannelRequest"/>, Delete the <see cref="Entities.HubsChannel"/> base on data <see cref="DeleteHubsChannelRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteHubsChannelRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteHubsChannelValidator();
            validator.ValidateAndThrow(request);

            // Find HubsChannel from database, throw NotFoundException when not found
            var hubsChannel = await hubsChannelRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (hubsChannel == null)
                CustomException.ThrowNotFoundException(typeof(Entities.HubsChannel), MsgCode.ERR_HUBS_HUBS_CHANNEL_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked HubsChannel as Delete state
                hubsChannel.IsActived = (int)ActivedType.DELETE;
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
