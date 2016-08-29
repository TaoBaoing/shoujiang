using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class InterfacecalllogMap : EntityTypeConfiguration<Interfacecalllog>
    {
        /// <summary>
        /// 接口调用日志，可以在web.config里设置记录日志的开关
        /// </summary>
        public InterfacecalllogMap()
        {

            // Table & Column Mappings
            this.ToTable("interfacecalllog");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
