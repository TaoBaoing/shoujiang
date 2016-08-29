using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QCloudAPI_SDK.Center;
using QCloudAPI_SDK.Module;

namespace ThirdApi.qcloud.api
{
    public  class QCloudApi
    {
        static SortedDictionary<string, object> GetCommonConfig()
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            var QCloudSecretId = System.Configuration.ConfigurationManager.AppSettings["QCloudSecretId"];
            var QCloudSecretKey = System.Configuration.ConfigurationManager.AppSettings["QCloudSecretKey"];
           
            config["SecretId"] = QCloudSecretId;
            config["SecretKey"] = QCloudSecretKey;
            config["RequestMethod"] = "GET";
            /* 区域参数，可选: gz:广州; sh:上海; hk:香港; ca:北美;等。 */
            config["DefaultRegion"] = "bj";
            return config;
        }

        public static Dictionary<string,string> GetVideoInfo(string fileId)
        {
            var config = GetCommonConfig();
            QCloudAPIModuleCenter vodmodule = new QCloudAPIModuleCenter(new VodApi(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["fileId"] = fileId;


            var resJson = vodmodule.Call("DescribeVodPlayUrls", requestParams);
            try
            {
                var obj = JObject.Parse(resJson);
                if (obj.GetValue("code").ToString() == "0")
                {
                    return new Dictionary<string, string>() { {"code","0"}, {"message", obj.GetValue("playSet").ToString() } };
                }

                return new Dictionary<string, string>() { { "code", "-1" }, { "message", obj.GetValue("message").ToString() } };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, string>() { { "code", "-1" }, { "message", resJson } };
            }

        }


        public static string DeleteVideo(string fileId)
        {
            var config = GetCommonConfig();
            QCloudAPIModuleCenter vodmodule = new QCloudAPIModuleCenter(new VodApi(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["fileId"] = fileId;
            requestParams["priority"] = 0;
            var resJson = vodmodule.Call("DeleteVodFile", requestParams);
            //resJson = UnicodeToString(resJson);
            try
            {
                var obj = JObject.Parse(resJson);
                if (obj.GetValue("code").ToString() == "0")
                {
                    return "";
                }
                return obj.GetValue("message").ToString();
            }
            catch (Exception ex)
            {
                return resJson;
            }

//                    {
//                "code" : 0,
//    "message" : "",
//}
        }

        /// <summary>
        /// Unicode字符串转为正常字符串
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        private static string UnicodeToString(string srcText)
        {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;
            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(0, 6).Substring(2);
                src = src.Substring(6);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        }



        //public void QCloudApiTest()
        //{
        //    SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
        //    config["SecretId"] = "AKIDHaVh1JSCQ4CcabLDzPywGo7goMZljygA";
        //    config["SecretKey"] = "oChIxNrEDcTImOlOlB8atQz9demJaLld";
        //    config["RequestMethod"] = "GET";
        //    /* 区域参数，可选: gz:广州; sh:上海; hk:香港; ca:北美;等。 */
        //    config["DefaultRegion"] = "bj";

        //    bool willContinue = true;
        //    while (willContinue)
        //    {
        //        int offset = 0;
        //        //点播
        //        string fileName = "D:\\VID_20160527_191843.mp4";
        //        QCloudAPIModuleCenter vodmodule = new QCloudAPIModuleCenter(new Vod(), config);
        //        SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
        //        requestParams["fileName"] = fileName;
        //        var fileSha = FileSecurityUtil.SHA1File(fileName);
        //        requestParams["fileSha"] = fileSha;
        //        requestParams["fileSize"] = 23105536;
        //        requestParams["dataSize"] = 1024 * 1024;//1M
        //        requestParams["offset"] = offset;
        //        requestParams["fileType"] = "mp4";
        //        //requestParams["isTranscode"] = 1;

        //        var res = vodmodule.Call("MultipartUploadVodFile", requestParams);
        //        willContinue = false;
        //    }
        //}
    }
}
