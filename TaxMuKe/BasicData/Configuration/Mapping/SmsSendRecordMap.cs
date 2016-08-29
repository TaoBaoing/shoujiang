using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class SmsSendRecordMap : EntityTypeConfiguration<MuKeSmsSendRecord>
    {
        public SmsSendRecordMap()
        {
            // Table & Column Mappings
            this.ToTable("muke_sms_send_record");

        }
    }
}
