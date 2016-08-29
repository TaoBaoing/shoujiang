using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicData
{
    public partial class MuKeMaterial
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string LinkUrl { get; set; }

        [NotMapped]
        public string RealLinkUrl { get; set; }
        public string OriginalVideoUrl { get; set; }

        public string Description { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }

        public MaterialTypes MaterialType { get; set; }

        public string QCloudTransCodeCallBack { get; set; }//腾讯视频转码成功后回调的数据
        public string QCloudServerFileId { get; set; }
        public int vbitrate { get; set; }
        public int vheight { get; set; }
        public int vwidth { get; set; }

        public bool transcodedone { get; set; }
        public long videoduration { get; set; }
    }
}
