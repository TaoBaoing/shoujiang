using BasicData;
using BasicData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels.Requester
{
    public class SearchMuKeUserOrderRequester : PagedRequesterBase
    {
        /// <summary>
        /// 服务器端唯一订单号
        /// </summary>
        public string out_trade_no { get; set; }


        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        public string buyer_email { get; set; }

        public string logicname { get; set; }
        public string phone { get; set; }
        public DateTime? txtStartTime { get; set; }
        public DateTime? txtEndTime { get; set; }


    }
}