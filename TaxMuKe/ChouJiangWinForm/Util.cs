using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ChouJiangWinForm
{
    public   class Util
    {
       
    }

    public class LuckyDto
    {
        public LuckyDto()
        {
            Details = new List<LuckyDetailDto>();
        }

        public string Code { get; set; }

        public List<LuckyDetailDto> Details { get; set; }

        public string LuckCode {
            get
            {
                var sb = new StringBuilder();
                foreach (LuckyDetailDto dto in Details)
                {
                    sb.Append(dto.Lucky+"  ");
                }
                return sb.ToString();
            }
        }
    }
    public class LuckyDetailDto
    {
        public int Lucky { get; set; }
    }
}
