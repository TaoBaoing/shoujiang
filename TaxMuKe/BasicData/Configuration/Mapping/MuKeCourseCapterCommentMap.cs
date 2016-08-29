﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicData.Domain.Course;

namespace BasicData.Configuration.Mapping
{
    public class MuKeCourseCapterCommentMap : EntityTypeConfiguration<MuKeCourseCapterComment>
    {
        public MuKeCourseCapterCommentMap()
        {
            this.ToTable("muke_course_capter_comment");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}
