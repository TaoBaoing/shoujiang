using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain;
using BasicUPMS.Models;

namespace BasicUPMS.Controllers
{
    public class MuKeServiceAgreementController:Controller
    {
        public ActionResult Index()
        {
            var set =
                SessionContext.Repository.MuKeGlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.ServiceAgreement);
            if (set == null)
            {
                set = new MuKeGlobalSet();
            }
            return View(set);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(string Content)
        {
            var set =
                SessionContext.Repository.MuKeGlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.ServiceAgreement);
            if (set == null)
            {
                set = new MuKeGlobalSet();
                set.GlobalSetType = GlobalSetType.ServiceAgreement;
                set.Content = Content;
                //set.MuKeUsersId = SessionContext.UserId;
                SessionContext.Repository.MuKeGlobalSets.Add(set);
            }
            else
            {
                set.Content = Content;
            }
            SessionContext.Repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}