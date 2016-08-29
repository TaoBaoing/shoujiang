using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;
using ThirdApi.qiniu.api;

namespace BasicUPMS.Controllers
{
    public class MuKeCourseBannerController : CURDTemplate<MuKeCourseBanner>
    {
        public ActionResult Index(SearchCourseBannerRequester request)
        {
            IQueryable<MuKeCourseBanner> queryableData = SessionContext.Repository.MuKeCourseBanners;

            // 状态
            if (request.Status.CompareTo(MuKeEnabledStatus.None) > 0)
            {
                queryableData = queryableData.Where(item => item.Status == request.Status);
            }

            var data =
                queryableData.OrderByDescending(i => i.Sort)
                    .ThenByDescending(i => i.CreateTime).ToPagedList(request.PageIndex, request.PageSize);
            foreach (var banner in data)
            {
                banner.ImageUrl = QiNiuApi.GetQiNiuUrlByKey(banner.ImageUrl);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_Items", data);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(MuKeCourseBanner requet)
        {
            var material = SessionContext.Repository.MuKeMaterials.FirstOrDefault(x => x.LinkUrl == requet.ImageUrl);
            if (material != null)
            {
                requet.Name = material.Name;
            }
            return base.Add(requet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Update([ModelBinder(typeof(UpdateableModelBinder))]MuKeCourseBanner requet)
        {
            var material = SessionContext.Repository.MuKeMaterials.FirstOrDefault(x => x.LinkUrl == requet.ImageUrl);
            if (material != null)
            {
                requet.Name = material.Name;
            }
            return base.Update(requet);
        }
    }
}