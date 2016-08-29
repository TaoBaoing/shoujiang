using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Configuration.Mapping
{
    public class MuKeCourseCapterTestPagperManagerMap:EntityTypeConfiguration<MuKeCourseCapterTestPagperManager>
    {
        public MuKeCourseCapterTestPagperManagerMap()
        {
            // Table & Column Mappings
            this.ToTable("muke_course_capter_testpagper_manager");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(t => t.Course)
            .WithMany(t => t.MuKeCourseCapterTestPagperManagers)
             .HasForeignKey(d => d.CourseId);

        }
    }
}
