using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Configuration.Mapping
{
    public class CourseViewRecordMap : EntityTypeConfiguration<CourseViewRecord>
    {
        public CourseViewRecordMap()
        {
            this.ToTable("courseview_record");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(x => x.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            this.HasRequired(t => t.User)
//             .WithMany(t => t.Historys)
             //.HasForeignKey(d => d.UserId);
        }
    }
}
