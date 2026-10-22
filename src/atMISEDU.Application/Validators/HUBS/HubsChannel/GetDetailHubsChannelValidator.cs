using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.HUBS.HubsChannel
{
    /// <summary>
    /// Validator for <see cref="GetDetailHubsChannelRequest"/>
    /// </summary>
    public class GetDetailHubsChannelValidator : Validator<GetDetailHubsChannelRequest>
    {
        public GetDetailHubsChannelValidator()
        {
            WithValidator(MsgCode.ERR_HUBS_HUBS_CHANNEL_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
