using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThirdApi.qcloud.api
{
   public   class QCloudPlaySet
    {
       public string url { get; set; }//url 
       public int definition { get; set; } //格式
        public int vbitrate { get; set; } //码率
        public int vheight { get; set; } //高度
        public int vwidth { get; set; } //宽度
    }
}
