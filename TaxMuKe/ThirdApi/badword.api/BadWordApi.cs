using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ThirdApi.badword.api
{
    public  class BadWordApi
    {
        /// <summary>
        /// 过滤脏词
        /// </summary>
        /// <param name="dirtyWords">脏词数组</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStrFilterBadWord(string value,string[] dirtyWords)
        {
//            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
//
//            string filepath = path+"badword.txt";
 
            FilterWord filter = new FilterWord(dirtyWords);
            filter.SourctText = value;
            string msg = filter.Filter('*');
            return msg;
        }
    }
}
