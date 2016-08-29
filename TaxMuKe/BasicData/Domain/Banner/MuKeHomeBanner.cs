using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
   public   class MuKeHomeBanner
    {
        public long Id { get; set; }

       public string Name { get; set; }
        public string ImageUrl { get; set; }
        public long ObjectId { get; set; }
        public MuKeHomeBannerTypes MuKeHomeBannerType { get; set; }
        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }

        public MuKeEnabledStatus Status { get; set; }
    }

    public enum MuKeHomeBannerTypes
    {
        [Description("全部")]
        None = 0,

        [Description("资讯")]
        Advice = 1,

        [Description("杂志")]
        Pdf = 2,
            
        [Description("课程")]
        Course = 3,

        [Description("案例")]
        Case = 4,

    }
}
