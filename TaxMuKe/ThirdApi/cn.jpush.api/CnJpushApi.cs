using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;

namespace ThirdApi.cn.jpush.api
{
    public class CnJpushApi
    {
        static readonly string CnJpushAppKey = System.Configuration.ConfigurationManager.AppSettings["CnJpushAppKey"];
        static readonly string CnJpushMasterSecret = System.Configuration.ConfigurationManager.AppSettings["CnJpushMasterSecret"];


        /// <summary>
        /// 发送给所有
        /// </summary>
        /// <param name="jpushType"></param>
        /// <param name="billId"></param>
        /// <param name="titleAlert"></param>
        /// <param name="webviewUrl"></param>
        /// <param name="sound"></param>
        public static void SendMessageToAll(int jpushType, long billId, string titleAlert,string webviewUrl="", string sound = "")
        {
            SendMessage(jpushType, billId, titleAlert,null,null,webviewUrl,sound);
        }

        /// <summary>
        /// 分组推送
        /// </summary>
        /// <param name="jpushType"></param>
        /// <param name="billId"></param>
        /// <param name="titleAlert"></param>
        /// <param name="tags"></param>
        /// <param name="sound"></param>
        public static void SendMessageToUserByTag(int jpushType, long billId, string titleAlert, HashSet<string> tags, string sound = "")
        {
            SendMessage(jpushType, billId, titleAlert, tags,"","",sound);
        }

        /// <summary>
        /// 推送给某人
        /// </summary>
        /// <param name="jpushType"></param>
        /// <param name="billId"></param>
        /// <param name="titleAlert"></param>
        /// <param name="userId"></param>
        /// <param name="sound"></param>
        /// <param name="webviewUrl"></param>
        public static void SendMessageToUserByAlias(int jpushType, long billId, string titleAlert, long userId, string sound = "",string webviewUrl="")
        {
            SendMessage(jpushType, billId, titleAlert,  null, userId.ToString(), webviewUrl, sound);
        }


        /// <summary>
        /// 发送极光推送   tags  和 alias 不能同时有值，建议调用其他方法
        /// </summary>
        /// <param name="jpushType"></param>
        /// <param name="billId"></param>
        /// <param name="titleAlert"></param>
        /// <param name="webviewUrl"></param>
        /// <param name="tags"></param>
        /// <param name="alias"></param>
        /// <param name="sound"></param>
        public static void SendMessage(int jpushType, long billId, string titleAlert, HashSet<string> tags = null, string alias = "", string webviewUrl = "",string sound= "")
        {

            JPushClient pushClient = new JPushClient(CnJpushAppKey, CnJpushMasterSecret);

            AndroidNotification androidNotification = new AndroidNotification();
            //            androidNotification.setTitle(title);
            androidNotification.setAlert(titleAlert);

            androidNotification.AddExtra("jpushtype", jpushType.ToString());//推送类型  
            androidNotification.AddExtra("id", billId.ToString());
            if (!string.IsNullOrEmpty(webviewUrl))
            {
                androidNotification.AddExtra("webviewurl", webviewUrl);
            }

            IosNotification isoNotification = new IosNotification();
            isoNotification.setAlert(titleAlert);
            isoNotification.AddExtra("jpushtype", jpushType.ToString());//推送类型  4类
            isoNotification.AddExtra("id", billId.ToString());
            if (!string.IsNullOrEmpty(webviewUrl))
            {
                isoNotification.AddExtra("webviewurl", webviewUrl);
            }
           
            if (!string.IsNullOrEmpty(sound))
            {
                isoNotification.setSound(sound);
            }

            Notification notification = new Notification();
            notification.setAndroid(androidNotification);
            notification.setIos(isoNotification);

             PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            if (tags == null && alias == null)
            {
                payload.audience = Audience.all();
            }
            if (tags != null)
            {
                payload.audience = Audience.s_tag(tags);
            }
            if (!string.IsNullOrWhiteSpace(alias))
            {
                payload.audience = Audience.s_alias(alias);
            }

            //payload.message = Message.content(dmo.Content);

            payload.notification = notification;
            var result = pushClient.SendPush(payload);
            if (!result.isResultOK())
            {
                throw new Exception("推送消息失败：" + result.ToString());
            }
        }
    }
}
