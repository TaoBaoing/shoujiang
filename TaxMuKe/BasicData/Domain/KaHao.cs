using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{
    public class KaHao
    {
        public long Id { get; set; }

        public string Code { get; set; }
        public string XingMing { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ShouKaRen { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
