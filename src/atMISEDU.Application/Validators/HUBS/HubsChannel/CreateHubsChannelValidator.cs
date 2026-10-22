using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.HUBS;

namespace atMISEDU.Application.Validators.HUBS.HubsChannel
{
    /// <summary>
    /// Validator for <see cref="CreateHubsChannelRequest"/>
    /// </summary>
    public class CreateHubsChannelValidator : Validator<CreateHubsChannelRequest>
    {
        /// <summary>
        /// Constructor of <see cref="CreateHubsChannelValidator"/>, register validator rules for <see cref="CreateHubsChannelRequest"/>
        /// </summary>
        public CreateHubsChannelValidator()
        {
            WithValidator(MsgCode.ERR_HUBS_HUBS_CHANNEL_INVALID);
            RuleFor(x => x.CnName).NotNull().MaxLength(HubsChannelConst.CN_NAME_MAX_LENGTH);
            RuleFor(x => x.CnUrl).NotNull().MaxLength(HubsChannelConst.CN_URL_MAX_LENGTH);
            RuleFor(x => x.CnIcon).NotNull().MaxLength(HubsChannelConst.CN_ICON_MAX_LENGTH);
            RuleFor(x => x.CnStyle).NotNull().MaxLength(HubsChannelConst.CN_STYLE_MAX_LENGTH);
            RuleFor(x => x.CnOrder).NotNull().GreaterThan(0);
            RuleFor(x => x.IsActived).NotNull().GreaterThan(0);
        }
    }
}
