using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class InterfacecalllogMap : EntityTypeConfiguration<Interfacecalllog>
    {
        /// <summary>
        /// �ӿڵ�����־��������web.config�����ü�¼��־�Ŀ���
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
