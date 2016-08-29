using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace ThirdApi.topsdk.api
{
   public  class TopSdkApi
   {    
       private static readonly string Url = "http://gw.api.taobao.com/router/rest"; 
       private static readonly string AppKey = "23384577";
       private static readonly string AppSecret = "9378333f93c70755b7dfe3fffd4fcfa1";
        //http://open.taobao.com/doc2/apiDetail.htm?spm=0.0.0.0.pjxLXY&apiId=25450


        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="msgcode"></param>
        /// <param name="phone"></param>
        public static AlibabaAliqinFcSmsNumSendResponse SendMes(string msgcode,string phone)
       {
        
            ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = "";
            req.SmsType = "normal";
            req.SmsFreeSignName = "身份验证"; //签名
            req.SmsParam = "{\"code\":\""+ msgcode + "\",\"product\":\"燕园财税\"}";
            //req.SmsParam = "{\"phone\":\""+ msgcode + "\"}";
            req.RecNum = phone;
            req.SmsTemplateCode = "SMS_10416108";//模板ID  SMS_10416108:身份验证
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);

            return rsp;
//            var ec = rsp.ErrCode;
//            var rb = rsp.Body;
            // Console.WriteLine(rsp.Body);
       }
    }
}
