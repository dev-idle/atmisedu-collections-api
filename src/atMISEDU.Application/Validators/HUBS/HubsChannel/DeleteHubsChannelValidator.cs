using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.HUBS.HubsChannel
{
    /// <summary>
    /// Validator for <see cref="DeleteHubsChannelRequest"/>
    /// </summary>
    public class DeleteHubsChannelValidator : Validator<DeleteHubsChannelRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteHubsChannelValidator"/>, register validator rules for <see cref="DeleteHubsChannelRequest"/>
        /// </summary>
        public DeleteHubsChannelValidator()
        {
            WithValidator(MsgCode.ERR_HUBS_HUBS_CHANNEL_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
