using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;

namespace BasicUPMS.Controllers
{
    public class KaHaoRecordController : CURDTemplate<KaHaoRecord>
    {
        public class LuckyDto
        {
            public LuckyDto()
            {
                Details=new List<LuckyDetailDto>();
            }

            public string Code { get; set; }

            public List<LuckyDetailDto> Details{ get; set; }

        }
        public class LuckyDetailDto
        {
            public int Lucky { get; set; } 
        }

        [AllowAnonymous]
        public ActionResult GetCanUseLucky()
        {
            List<LuckyDto> luckys=new List<LuckyDto>();
           var group= SessionContext.Repository.KaHaoRecords.OrderBy(x=>x.CreateTime).ThenBy(x=>x.Id).Include(x => x.KaHao).GroupBy(x => x.KaHao.Code).ToList();
            foreach (var ks in group)
            {
                if (ks.Count() == 10)
                {
                    var luck = new LuckyDto();
                    luck.Code = ks.Key;
                    foreach (KaHaoRecord haoRecord in ks)
                    {
                        luck.Details.Add(new LuckyDetailDto() {Lucky = haoRecord.Lucky});
                    }
                    luckys.Add(luck);
                }
            }

            return Json(luckys, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(KaHaoRecordRequester requester)
        {
            IQueryable<KaHaoRecord> list = SessionContext.Repository.KaHaoRecords.Include(x=>x.KaHao).Include(x=>x.MuKeDirtyWord).OrderBy(x => x.CreateTime).ThenBy(x=>x.Id);
            if (!string.IsNullOrEmpty(requester.Code))
            {
                list = list.Where(x => x.KaHao.Code == requester.Code);
            }

            var data = list.ToPagedList(requester.PageIndex, requester.PageSize);
            ViewBag.PageIndex = requester.PageIndex;
            ViewBag.PageSize = requester.PageSize;
            if (Request.IsAjaxRequest())
                return PartialView("_Items", data);
            return View(data);
        }

        public override PartialViewResult FormForAdd(string viewName, KaHaoRecord entity)
        {
            var luck = new Random().Next(0, 9);
            ViewBag.Luck = luck;
            return base.FormForAdd(viewName, entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(KaHaoRecord requet)
        {
            var shangjia =
                SessionContext.Repository.MuKeDirtyWords.FirstOrDefault(x => x.Id == SessionContext.Principal.RoleId);
            if (shangjia == null)
            {
                return Json(new ResultModel<long>(false, -1, "不是商家账号", 0));
            }
            requet.MuKeDirtyWordId = shangjia.Id;
         
            var kahao = SessionContext.Repository.KaHaos.FirstOrDefault(x => x.Code == requet.Code);
            if (kahao == null)
            {
                return Json(new ResultModel<long>(false, -1, "此卡号不存在", 0));
            }
            requet.KaHaoId = kahao.Id;

            return base.Add(requet);
        }
    }
}