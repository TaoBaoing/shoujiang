using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicData;

namespace BasicUPMS.Models
{
    internal static class SessionContext
    {
        internal static UcfUniversityContext Repository
        {
            get
            {
                if (!HttpContext.Current.Items.Contains("dbContext"))
                {
                    HttpContext.Current.Items.Add("dbContext", new UcfUniversityContext());
                }
                return HttpContext.Current.Items["dbContext"] as UcfUniversityContext;
            }
        }
        internal static UPMSPrincipal Principal
        {
            get
            {
                return HttpContext.Current.User as UPMSPrincipal;
            }
        }

        internal static long UserId
        {
            get
            {
                long id;
                if (long.TryParse(Principal.Identity.Name, out id))
                {
                    return id;
                }
                return 0;
            }
        }
        internal static ILog Logger
        {
            get
            {
                return LogManager.GetLogger("log4net");
            }
        }
    }
}