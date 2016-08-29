using BasicData;
using BasicFramework.Component;
using BasicUPMS.Models;
using System.Linq;
using System.Web.Mvc;
using BasicCommon;
using ThirdApi.qiniu.api;

namespace BasicUPMS.Controllers
{
    public class BannerController : CURDTemplate<MuKeBanner>
    {
        public ActionResult Index(SearchBannerRequester request)
        {
            // 轮播图信息
            IQueryable<MuKeBanner> queryableData = SessionContext.Repository.MuKeBanners;
            if (request.BannerType != MuKeBannerTypes.None)
            {
                queryableData = queryableData.Where(i => i.BannerType == request.BannerType);
            }
            if (request.ObjectId > 0)
            {
                queryableData = queryableData.Where(i => i.ObjectId == request.ObjectId);
            }
            var data =
            queryableData.OrderByDescending(i => i.Sort)
            .ThenByDescending(i => i.CreateTime)
            .ToPagedList(request.PageIndex, request.PageSize);

            //ViewBag.BannerType = request.BannerType;
            ViewBag.BannerType =MuKeBannerTypes.Home;//目前写死
            ViewBag.ObjectId = request.ObjectId;

            foreach (var banner in data)
            {
                banner.ImageUrl =QiNiuApi.GetQiNiuUrlByKey(banner.ImageUrl);
            }
            if (Request.IsAjaxRequest())
                return PartialView("_Items", data);
            return View(data);
        }

    }
}
