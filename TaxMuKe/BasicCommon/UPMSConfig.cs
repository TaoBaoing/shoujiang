using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BasicCommon
{
    public class UPMSConfig
    {
        public static int MenuDepth
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MenuDepth"]);
            }
        }

        /// <summary>
        /// API域名
        /// </summary>
        public static string ApiDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiDomain"];
            }
        }
        public static int FeatureDepth
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["FeatureDepth"]);
            }
        }
        public static int CountDepth
        {
            get
            {
                return MenuDepth + FeatureDepth;
            }
        }
        public static bool Debug
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["Debug"]);
            }
        }
        public static string SystemAdminPassport
        {
            get
            {
                return ConfigurationManager.AppSettings["SystemAdminPassport"];
            }
        }
        public static int SystemRole
        {
            get
            {
                return 888888;
            }
        }
        public static int DefaultPageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["DefaultPageSize"]);
            }
        }
        public static string DefaulPassword
        {
            get
            {
                return "123456";
            }
        }
        public static bool CkeckUrlReferrer
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["CkeckUrlReferrer"]);
            }
        }
        public static string WebSite
        {
            get
            {
                return ConfigurationManager.AppSettings["WebSite"];
            }
        }
        public static string FilePhysicalPath
        {
            get
            {
                return ConfigurationManager.AppSettings["FilePhysicalPath"];
            }
        }

        /// <summary>
        /// 短信通道ID
        /// </summary>
        public static long SmsChannelId
        {
            get
            {
                return long.Parse(ConfigurationManager.AppSettings["SmsChannelId"]);
            }
        }

        /// <summary>
        /// 短信模板
        /// </summary>
        public static string SmsValidationTemplete
        {
            get
            {
                return ConfigurationManager.AppSettings["SmsValidationTemplete"];
            }
        }

        /// <summary>
        /// 七牛空间名
        /// </summary>
        public static string QiNiuBucket
        {
            get
            {
                return ConfigurationManager.AppSettings["QiNiuBucket"];
            }
        }

        /// <summary>
        /// 七牛域名
        /// </summary>
        public static string QiNiuDomainName
        {
            get
            {
                return ConfigurationManager.AppSettings["QiNiuDomainName"];
            }
        }

 

        /// <summary>
        /// 腾讯视频 SecretId
        /// </summary>
        public static string QCloudSecretId
        {
            get
            {
                return ConfigurationManager.AppSettings["QCloudSecretId"];
            }
        }
        /// <summary>
        /// 腾讯视频 SecretKey
        /// </summary>
        public static string QCloudSecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["QCloudSecretKey"];
            }
        }

    }
}