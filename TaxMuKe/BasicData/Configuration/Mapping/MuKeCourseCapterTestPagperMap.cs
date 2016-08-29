using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Configuration.Mapping
{
    public  class MuKeCourseCapterTestPagperMap : EntityTypeConfiguration<MuKeCourseCapterTestPagper>
    {
        public MuKeCourseCapterTestPagperMap()
        {
            // Table & Column Mappings
            this.ToTable("muke_course_capter_testpagper");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(t => t.MuKeCourseCapterTestPagperManager)
            .WithMany(t => t.MuKeCourseCapterTestPagpers)
             .HasForeignKey(d => d.MuKeCourseCapterTestPagperManagerId);
            this.Ignore(x => x.AnswerAIsSelected);
            this.Ignore(x => x.AnswerBIsSelected);
            this.Ignore(x => x.AnswerCIsSelected);
            this.Ignore(x => x.AnswerDIsSelected);

        }
    }
}
