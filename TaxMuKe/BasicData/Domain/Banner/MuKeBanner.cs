using System;
using System.Collections.Generic;

namespace BasicData
{
    public partial class MuKeBanner
    {
        public long Id { get; set; }

        public long ObjectId { get; set; }
        public MuKeBannerTypes BannerType { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
        public string Attribute { get; set; }
        public string Description { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public int Sort { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
