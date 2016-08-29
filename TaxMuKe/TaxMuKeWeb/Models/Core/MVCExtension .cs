using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BasicUPMS.Models
{
    public static class MVCExtension 
    {
        /// <summary>
        /// 拓展方法，获取ModelState的错误信息（冒泡显示）
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string PopErrorMessage(this ModelStateDictionary modelState)
        {
            return modelState.Values.Where(i => i.Errors.Count > 0).FirstOrDefault().Errors[0].ErrorMessage;
        }

        /// <summary>
        /// 获取枚举描述（暂且用反射的方式）
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static string EnumDescription(this HtmlHelper htmlHelper, Enum selected)
        {
            return ((DescriptionAttribute)selected.GetType().GetField(selected.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).First()).Description;
        }
        public static List<SelectListItem> SelectListItems(this HtmlHelper htmlHelper, Enum selected, bool isDisplayNone)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            // 获取枚举字段
            var fields = selected.GetType().GetFields().Where(i => i.IsStatic);

            //是否有选中项
            var isSelected = false;

            foreach (var field in fields)
            {
                // 获取枚举值
                int value = (int)selected.GetType().InvokeMember(field.Name, BindingFlags.GetField, null, null, null);

                if (!isDisplayNone)
                {
                    if (value == 0)
                    {
                        continue;
                    }
                }
                string text = string.Empty;
                object[] array = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (array.Length > 0) text = ((DescriptionAttribute)array[0]).Description;
                else text = field.Name;

                //当前项是否选中
                bool currentIsSelected = field.Name == selected.ToString();

                if (!isSelected)
                {
                    isSelected = currentIsSelected;
                }
                list.Add(new SelectListItem { Value = value.ToString(), Text = text, Selected = currentIsSelected });

            }
            //如果没有选中项,选中第一个
            if (!isSelected)
            {
                list.FirstOrDefault().Selected = true;
            }

            return list;
        }


        /// <summary>
        /// html解码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string MvcHtmlDecode(this HtmlHelper helper, string html)
        {
            return HttpContext.Current.Server.HtmlDecode(html);
        }
    }
}
