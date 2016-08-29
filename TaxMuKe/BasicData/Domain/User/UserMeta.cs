using System;
using System.Collections.Generic;

namespace BasicData
{
    public partial class UserMeta
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual MuKeUsers User { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}
