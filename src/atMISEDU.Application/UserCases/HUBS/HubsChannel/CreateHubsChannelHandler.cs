using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Application.Validators.HUBS.HubsChannel;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.HUBS;

namespace atMISEDU.Application.UserCases.HUBS.HubsChannel
{
    /// <summary>
    /// Handler for <see cref="CreateHubsChannelRequest"/> 
    /// </summary>
    public class CreateHubsChannelHandler : IRequestHandler<CreateHubsChannelRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.HubsChannel"/>>
        private readonly IHubsChannelSqlRepository hubsChannelRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateHubsChannelHandler"/>, inject needed dependency
        /// </summary>
        public CreateHubsChannelHandler(IHubsChannelSqlRepository hubsChannelRepo, ISqlUnitOfWork unitOfWork)
        {
            this.hubsChannelRepo = hubsChannelRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateHubsChannelRequest"/>, Create the <see cref="Entities.HubsChannel"/> base on data <see cref="CreateHubsChannelRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateHubsChannelRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new CreateHubsChannelValidator();
            validator.ValidateAndThrow(request);

            // Create new HubsChannel from request
            var hubsChannel = request.MapTo<Entities.HubsChannel>();

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked HubsChannel as Create state
                hubsChannelRepo.Add(hubsChannel);
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
