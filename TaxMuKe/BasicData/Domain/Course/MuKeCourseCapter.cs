using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicData
{
    public partial class MuKeCourseCapter
    {
        public long Id { get; set; }
        
        public string ItemImage { get; set; }//ËõÂÔÍ¼

        public string Remark { get; set; }//ÃèÊö

        public virtual MuKeCourse Course { get; set; }

        public long CourseId { get; set; }
        public string Title { get; set; }
        public string PlayUrl { get; set; }
        public string DownLoadUrl { get; set; }
        public int Sort { get; set; }
        public int Integral { get; set; }
        public long TimeLength { get; set; }

        [NotMapped]
        public string TimeLengthDisplay { get; set; }
        public ItemContentType ContentType { get; set; }
        public string Content { get; set; }//->ÕÂ½ÚËµÃ÷
        public MuKeEnabledStatus Status { get; set; }
        public int ClickCount { get; set; }
    }
}
