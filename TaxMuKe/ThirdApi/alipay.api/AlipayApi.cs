using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using ThirdApi.alipay.api.Source;

namespace ThirdApi.alipay.api
{
   public    class AlipayApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outTradeNo">支付宝合作商户网站唯一订单号</param>
        /// <returns></returns>
        public static string GetOrderInfo(string outTradeNo,string subject,string body,string totalFee,string notifyUrl,string orderType)
        {
            var infoDic=new Dictionary<string, string>();
            infoDic.Add("partner", AlipayConfig.Partner);
            infoDic.Add("seller_id", AlipayConfig.SellerId);
            infoDic.Add("out_trade_no", outTradeNo);
            infoDic.Add("subject", subject);
            infoDic.Add("body", body);
            infoDic.Add("total_fee", totalFee);
            infoDic.Add("notify_url", notifyUrl);
            infoDic.Add("service", "mobile.securitypay.pay");
            infoDic.Add("payment_type", "1");
            infoDic.Add("_input_charset", AlipayConfig.Input_charset);
            infoDic.Add("it_b_pay", "30m");
            infoDic.Add("order_type", orderType);
            var content= AlipayCore.CreateLinkString(infoDic);
//            var content= AlipayCore.CreateLinkUrlString(infoDic);
         
           var sign= RSAFromPkcs8.sign(content, AlipayConfig.Private_key.Trim(), "utf-8");
            infoDic.Add("sign", sign);
            infoDic.Add("sign_type","RSA");
            

            var orderInfo = AlipayCore.CreateLinkString(infoDic);
//            var orderInfo = AlipayCore.CreateLinkUrlString(infoDic);

            return orderInfo;
        }
    }
}
