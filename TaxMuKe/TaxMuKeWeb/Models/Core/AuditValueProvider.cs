using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasicUPMS.Models
{
    /// <summary>
    /// 提供审计信息
    /// </summary>
    public class AuditValueProvider : IValueProvider
    {
        public bool ContainsPrefix(string prefix)
        {
            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            string currentUserId = ((FormsIdentity)SessionContext.Principal.Identity).Label;
            switch (key)
            {
                case "UpdateUser": return new ValueProviderResult(long.Parse(currentUserId), currentUserId, CultureInfo.InvariantCulture);
                case "UpdateTime": return new ValueProviderResult(DateTime.Now, DateTime.Now.ToString(), CultureInfo.InvariantCulture);
                default: return null;
            }
        }
    }
}