using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData;

namespace BasicUPMS.Controllers
{
    public class CURDTemplate<T> : BaseDataController where T : class,new()
    {
        public virtual PartialViewResult FormForAdd(string viewName, T entity)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                viewName = "_Form";
            }
            ViewBag.OperateType = OperateTypes.Add;
            return PartialView(viewName, entity);
        }

        public virtual PartialViewResult FormForUpdateWithIndex(string viewName, long id, string sucessIndex)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                viewName = "_Form";
            }
            var entity = SessionContext.Repository.Set<T>().Find(id);
            ViewBag.OperateType = OperateTypes.Update;
            return PartialView(viewName, entity);
        }

        public virtual PartialViewResult FormForUpdate(string viewName, long id)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                viewName = "_Form";
            }
            var entity = SessionContext.Repository.Set<T>().Find(id);
            ViewBag.OperateType = OperateTypes.Update;
            return PartialView(viewName, entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual JsonResult Add(T requet)
        {
            var result = CheckModelCallback<T>(() =>
              {
                  return SessionContext.Repository.Set<T>().Add(requet);
              });
            return Json(new ResultModel<long>(result.IsSuccess, result.Code, result.Message, result.IsSuccess ? ((dynamic)result.Data).Id : 0));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual JsonResult Update([ModelBinder(typeof(UpdateableModelBinder))]T requet)
        {
           return Json(CheckModelCallback(() => { }));
        }
        [HttpPost]
        public virtual JsonResult Remove(long id)
        {
            var result = CheckParameterCallback<long>(() =>
                     {
                         var entity = SessionContext.Repository.Set<T>().Find(id);
                         if (entity != null)
                         {
                             SessionContext.Repository.Set<T>().Remove(entity);
                             return 0;
                         }
                         return -1;
                     }, id);
            if (result.IsSuccess && result.Data == -1)
            {
                result.IsSuccess = false;
                result.Message = ErrorMessage.ArgumentError;
            }
            return Json(result);
        }
    }
}