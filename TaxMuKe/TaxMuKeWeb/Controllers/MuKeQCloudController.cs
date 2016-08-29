using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicUPMS.Controllers
{
    [AllowAnonymous]
    public class MuKeQCloudController : Controller
    {
        // GET: MuKeQCloud
        public ActionResult Index()
        {
            return View();
        }
    }
}