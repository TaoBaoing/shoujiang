using System;
using System.Collections.Generic;

namespace BasicData
{
    public partial class Settings
    {
        public long Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
