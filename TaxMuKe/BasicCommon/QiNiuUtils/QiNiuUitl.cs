using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BasicCommon
{
    public  class QiNiuUitl
    {
        /// <summary>
        /// 获取七牛私密空间M3U8文件签名地址
        /// </summary>
        /// <param name="qiNiuPath">七牛文件路径</param>
        /// <param name="accessKey">AccessKey</param>
        /// <param name="secretKey">SecretKey</param>
        /// <param name="effectiveMinutes">有效时间（单位：分钟）</param>
        /// <returns></returns>
        public static string GetPrivateM3U8(string qiNiuPath,double effectiveMinutes = 12 * 60, string accessKey ="",string secretKey="")
        {

            if (string.IsNullOrEmpty(accessKey))
            {
                accessKey= ConfigurationManager.AppSettings["QiNiuAccessKey"];
            }
            if (string.IsNullOrEmpty(secretKey))
            {
                secretKey= ConfigurationManager.AppSettings["QiNiuSecretKey"];
            }

            var dt = DateTime.Now.AddMinutes(effectiveMinutes);
            var timeStamp = GetUnixTime(dt);

            var url = qiNiuPath + "?pm3u8/0/"+ (effectiveMinutes*60);//默认43200=12小时

            url += "&e=" + timeStamp;
           
            //var skey = "frbwKh2eCWnL3mNs0pd7yW89_v6Ql1w5TZzwj1ZS";

            //var akey = "2qd72aY-dswVb1DW8hLrja1V5CvhDLhAiWse9RC5";

            var base64Skey = HmacSha1Sign(url, secretKey); //FYvdY8fjBKMxkvdboBFHj6YYeOI=
            url += "&token=" + accessKey + ":" + base64Skey;
            return url;
        }

        private static string HmacSha1Sign(string url, string skey)
        {
            Encoding encodeing = Encoding.UTF8;
            byte[] byteData = encodeing.GetBytes(url);
            byte[] byteKey = encodeing.GetBytes(skey);
            HMACSHA1 hmac = new HMACSHA1(byteKey);
            CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();
            var base64 = Convert.ToBase64String(hmac.Hash).Replace("+", "-").Replace("/", "_");
            return base64;
        }

        private static int GetUnixTime(DateTime dt)
        {
            //DateTime dtStart = new DateTime(1970, 1, 1, 0, 0, 0);//直接转换，不改变时区
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            TimeSpan toNow = dt.Subtract(dtStart);
            int timeStamp = (int)(toNow.Ticks / 10000000);
            return timeStamp;
            
        }
       
    }
}
