using System;
using System.Collections.Generic;

namespace BasicData
{
    /// <summary>
    /// 短信服务通道
    /// </summary>
    public partial class MuKeSmsChannel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public int PerMobilePerHourLimit { get; set; }
        public int PerMobilePerDayLimit { get; set; }
        public int PerMobileMinInterval { get; set; }

        public MuKeEnabledStatus Status { get; set; }

    }
}
