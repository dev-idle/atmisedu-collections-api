using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.HUBS;

namespace atMISEDU.Application.Validators.HUBS.HubsChannel
{
    /// <summary>
    /// Validator for <see cref="UpdateHubsChannelRequest"/>
    /// </summary>
    public class UpdateHubsChannelValidator : Validator<UpdateHubsChannelRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateHubsChannelValidator"/>, register validator rules for <see cref="UpdateHubsChannelRequest"/>
        /// </summary>
        public UpdateHubsChannelValidator()
        {
            WithValidator(MsgCode.ERR_HUBS_HUBS_CHANNEL_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.CnName).MaxLength(HubsChannelConst.CN_NAME_MAX_LENGTH);
            RuleFor(x => x.CnUrl).MaxLength(HubsChannelConst.CN_URL_MAX_LENGTH);
            RuleFor(x => x.CnIcon).MaxLength(HubsChannelConst.CN_ICON_MAX_LENGTH);
            RuleFor(x => x.CnStyle).MaxLength(HubsChannelConst.CN_STYLE_MAX_LENGTH);
            RuleFor(x => x.CnOrder).GreaterThan(0);
            RuleFor(x => x.IsActived).GreaterThan(0);
        }
    }
}
