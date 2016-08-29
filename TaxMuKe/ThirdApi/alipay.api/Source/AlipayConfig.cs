using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace Com.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class AlipayConfig
    {
        #region 字段
        private static string partner = "2088421216001054";
        private static string seller_id = "2088421216001054";
        private static string private_key = @"MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBALcYYctc83D0atov1ZvVJ78yAOyKw7M/D7ZANjCnJAnTW8ZO8danVIQIEAe7Qh+531pHQgvR9SHStLWWM4sLsLW+4pfe/GSMB7PD1RFNWjBv6sU3QVUr0yi4xGC+KLFi0eJrapMyykLVVgMk4EIGjc/soHvIWVqDm/b6FmxD0AGjAgMBAAECgYB8vpzqfELUQ4KFcy+7A0gYSM4+WvCosB9ZUXhaP5YbJ6iQfPrh/5Fif0fErL2Jdg8JqCEEgqDb0X4TdelaI7XZhtCjiesGLWhUpafQVFtm5NfjB3Mh9Rr8X3Ge+Y/VXBjGob3u1Bffrqhl/7y6lZAkPWuqNeryHUsDFuwvg5dn2QJBANuVCTusERByU4W7otBh5Q1X7SiGpfwBFYrxtmuxYVZypQBtmyrefAq/Xj4d5iFB94hfVxkZv0Lg1hFbDxh1lv0CQQDVdjFzVu4teaBOobaonR2Rg4rZqoaAZcH1NME/r0g6ephlxfctMIPXBg0JGQuh/nacnSd/DZDISigGSp7qPW0fAkAynAIZx8nTEZagjyOyha2mSqq1mXsuPw25DYbvAV4e76OKGrkVw1uxmnPdO0TsS2kC/H51oZKMloegTm2HRKr9AkBpvTKhv94cCLOwP8sLm0iaXF7bk+0sjTuXC5ruwRU2YqQ7ReU7LV0hlo2+g/mPNU3sJqz58u5VSOaqtywhPGxhAkAJSIBqXeahqRCRy+NoUsOvo2BPvQX+don2wHHeOR3rhZqVDIehVbX3mKVHtiWHF2ZAHly1w9R9b+XIIK3wZLOh";
        private static string public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";
        private static string input_charset = "utf-8";
        private static string sign_type = "RSA";
        #endregion

        static AlipayConfig()
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = "2088421216001054";
            seller_id = "2088421216001054";

            //商户的私钥
            private_key = @"MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBALcYYctc83D0atov1ZvVJ78yAOyKw7M/D7ZANjCnJAnTW8ZO8danVIQIEAe7Qh+531pHQgvR9SHStLWWM4sLsLW+4pfe/GSMB7PD1RFNWjBv6sU3QVUr0yi4xGC+KLFi0eJrapMyykLVVgMk4EIGjc/soHvIWVqDm/b6FmxD0AGjAgMBAAECgYB8vpzqfELUQ4KFcy+7A0gYSM4+WvCosB9ZUXhaP5YbJ6iQfPrh/5Fif0fErL2Jdg8JqCEEgqDb0X4TdelaI7XZhtCjiesGLWhUpafQVFtm5NfjB3Mh9Rr8X3Ge+Y/VXBjGob3u1Bffrqhl/7y6lZAkPWuqNeryHUsDFuwvg5dn2QJBANuVCTusERByU4W7otBh5Q1X7SiGpfwBFYrxtmuxYVZypQBtmyrefAq/Xj4d5iFB94hfVxkZv0Lg1hFbDxh1lv0CQQDVdjFzVu4teaBOobaonR2Rg4rZqoaAZcH1NME/r0g6ephlxfctMIPXBg0JGQuh/nacnSd/DZDISigGSp7qPW0fAkAynAIZx8nTEZagjyOyha2mSqq1mXsuPw25DYbvAV4e76OKGrkVw1uxmnPdO0TsS2kC/H51oZKMloegTm2HRKr9AkBpvTKhv94cCLOwP8sLm0iaXF7bk+0sjTuXC5ruwRU2YqQ7ReU7LV0hlo2+g/mPNU3sJqz58u5VSOaqtywhPGxhAkAJSIBqXeahqRCRy+NoUsOvo2BPvQX+don2wHHeOR3rhZqVDIehVbX3mKVHtiWHF2ZAHly1w9R9b+XIIK3wZLOh";

         
            public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";

            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑



            //字符编码格式 目前支持 gbk 或 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：RSA、DSA、MD5
            sign_type = "RSA";
        }

        #region 属性
        /// <summary>
        /// 支付宝账号
        /// </summary>
        public static string SellerId
        {
            get { return seller_id; }
            set { seller_id = value; }
        }
        
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        #endregion
    }
}