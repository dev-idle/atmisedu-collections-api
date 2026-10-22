using atMISEDU.Domain.Constants;
using atMISEDU.Domain.Constants.HUBS;
using atMISEDU.Domain.Entities.HUBS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace atMISEDU.Persistence.Configurations.HUBS
{
    public class HubsChannelConfiguration : IEntityTypeConfiguration<HubsChannel>
    {
        public void Configure(EntityTypeBuilder<HubsChannel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(HubsChannelConst.FIELD_ID);

            builder.Property(x => x.CnName).HasColumnName(HubsChannelConst.FIELD_CN_NAME);
            builder.Property(x => x.CnUrl).HasColumnName(HubsChannelConst.FIELD_CN_URL);
            builder.Property(x => x.CnIcon).HasColumnName(HubsChannelConst.FIELD_CN_ICON);
            builder.Property(x => x.CnStyle).HasColumnName(HubsChannelConst.FIELD_CN_STYLE);
            builder.Property(x => x.CnOrder).HasColumnName(HubsChannelConst.FIELD_CN_ORDER);
            builder.Property(x => x.IsActived).HasColumnName(HubsChannelConst.FIELD_IS_ACTIVED);

            builder.ToTable(TableConst.TABLE_HUBS_CHANNEL);
        }
    }
}
