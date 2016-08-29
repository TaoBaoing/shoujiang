using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCommon
{
    public static class StringExtend
    {
        /// <summary>
        /// 转换为日期显示格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDate(this object obj)
        {
            try
            {

                return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 转换为日期显示格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDateTime(this object obj)
        {
            try
            {
                return Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:MM:ss");
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
