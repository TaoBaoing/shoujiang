using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCommon
{
   public  class DateTimeUtil
    {
       public static string GetDateStrBySecond(long second)
       {
           if (second == 0)
           {
               return "0时0分0秒";
           }
           var hour = second/3600;
           var 剩下的秒数 = second -hour*3600;
           var min = 剩下的秒数/60;
           var sec = 剩下的秒数%60;

           return hour + "时" + min + "分" + sec + "秒";
       }
    }
}
