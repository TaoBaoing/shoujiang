using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class SmsRequestQueueMap : EntityTypeConfiguration<SmsRequestQueue>
    {
        public SmsRequestQueueMap()
        {
            // Table & Column Mappings
            this.ToTable("sms_request_queue");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
