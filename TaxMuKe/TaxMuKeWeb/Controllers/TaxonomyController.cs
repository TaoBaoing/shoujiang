using BasicCommon;
using BasicData;
using BasicUPMS.Models;
using BasicUPMS.Services;
using System.Web.Mvc;
using System.Web.Security;

namespace BasicUPMS.Controllers
{
    public class TaxonomyController : CURDTemplate<Taxonomy>
    {
        public ActionResult Index(TaxonomyTypes taxonomyType)
        {
            string template;
            switch (taxonomyType)
            {
                // 学习项目
                case TaxonomyTypes.LearningProjects:

                    template = "SingleLayer";
                    break;
                // 公开课
                case TaxonomyTypes.Course:
                // 大学活动
                case TaxonomyTypes.UniversityActivities:
                case TaxonomyTypes.Images:
                case TaxonomyTypes.Audio:
                case TaxonomyTypes.Video:
                case TaxonomyTypes.OtherFile:
                case TaxonomyTypes.Magazine:
                    template = "MultiLayer"; break;
                default: template = "SingleLayer"; break;
            }
            ViewBag.Taxonomys = taxonomyType;

            return View(template);
        }

        public JsonResult GetTreeData(long currentNodeId, TaxonomyTypes taxonomyType)
        {
            TaxonomyService termTaxonomyService = new TaxonomyService();
            var vm = termTaxonomyService.GetTaxonomyTree(0, UPMSConfig.CountDepth, currentNodeId, null, taxonomyType, MuKeEnabledStatus.None);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(Taxonomy requet)
        {
            requet.CreateUser = long.Parse(((FormsIdentity)User.Identity).Label);
            return base.Add(requet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Update(Taxonomy requet)
        {
            Taxonomy tt = SessionContext.Repository.Taxonomys.Find(requet.Id);
            // 更改姓名
            tt.Name = requet.Name;
            tt.Status = requet.Status;
            tt.Sort = requet.Sort;
            tt.Description = requet.Description;

            return Json(CheckModelCallback<long>(() => { return requet.ParentId; }));
        }
    }
}
