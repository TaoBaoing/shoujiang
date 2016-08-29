using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class SmsChannelMap : EntityTypeConfiguration<MuKeSmsChannel>
    {
        /// <summary>
        /// 
        /// </summary>
        public SmsChannelMap()
        {           
            // Table & Column Mappings
            this.ToTable("muke_sms_channel");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
