using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class CourseItemMap : EntityTypeConfiguration<MuKeCourseCapter>
    {
        public CourseItemMap()
        {
            // Table & Column Mappings
            this.ToTable("muke_course_capter");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(t => t.Course)
            .WithMany(t => t.MetaData)
             .HasForeignKey(d => d.CourseId);

        }
    }
}
