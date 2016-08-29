using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BasicUPMS.Models
{
    public class CustomValueProviderFactory : System.Web.Mvc.ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new AuditValueProvider();
        }
    }
}