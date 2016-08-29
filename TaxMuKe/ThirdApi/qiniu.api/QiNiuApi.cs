using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qiniu.IO;
using Qiniu.RS;

namespace ThirdApi.qiniu.api
{
    public  class QiNiuApi
    {

        public static readonly string Pre_UserAvatarUri = "useravataruri";
        public static readonly string Pre_Taxonomy = "taxonomy";


        public static string GetQiNiuUrlByKey(string qiniukey, string qiNiuDomainName = "")
        {
            if (string.IsNullOrEmpty(qiNiuDomainName))
            {
                if (System.Configuration.ConfigurationManager.AppSettings["QiNiuDomainName"] != null)
                {
                    qiNiuDomainName = System.Configuration.ConfigurationManager.AppSettings["QiNiuDomainName"];
                }
            }
            return "http://" + qiNiuDomainName + "/" +  qiniukey;
        }

        /// <summary>
        ///上传小文件到七牛，不分块上传
        /// </summary>
        /// <param name="bucket">七牛空间名</param>
        /// <param name="key">上传到七牛后的key（文件名）</param>
        /// <param name="fileName">本地文件地址</param>
        /// <returns></returns>
        public static PutRet UpLoadMinFile(string bucket,string key,string fileName)
        {
            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();
            //设置上传的空间
            //String bucket = "taxmuke";
            //设置上传的文件的key值
            //String key = "home/banner/mainsystem.png";

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy put = new PutPolicy(bucket, 3600);

            //调用Token()方法生成上传的Token
            string upToken = put.Token();
            //上传文件的路径
            //String fileName = @"D:\mainsystem.png";

            //调用PutFile()方法上传
            PutRet ret = target.PutFile(upToken, key, fileName, extra);

            return ret;
            // return View();
        }
    }
}
