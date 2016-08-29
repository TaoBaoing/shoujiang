using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Resources;
using System;
using System.Linq;

namespace BasicUPMS.Controllers
{
    public class BaseDataController : BaseController
    {
        #region Nomal Method
        public ResultModel SaveChanges()
        {
            if (SessionContext.Repository.SaveChanges() > 0)
                return new ResultModel(true);
            return new ResultModel(false, -2, ErrorMessage.DBNoChanges);
        }
      
        public ResultModel CheckModelCallback(Action callback)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            //参数验证通过
            if (ModelState.IsValid)
            {
                callback.Invoke();
                return SaveChanges();
            }
            return new ResultModel(false, -1, ModelState.PopErrorMessage());
        }
        public ResultModel CheckParameterCallback(Action callback, params object[] parameters)
        {
            if (parameters != null)
            {
                bool pass = true;
                foreach (object parameter in parameters)
                {
                    if (
                        (((parameter is int) || (parameter is long)) && Convert.ToInt64(parameter) == 0) ||
                        ((parameter is string) && string.IsNullOrWhiteSpace(parameter.ToString()))
                        )
                    {
                        pass = false;
                        break;
                    }
                }
                if (!pass)
                {
                    return new ResultModel(false, -1, ErrorMessage.ArgumentError);
                }
            }
            callback.Invoke();
            return SaveChanges();
        }
        #endregion

        #region Generic Method
        public ResultModel<TData> SaveChanges<TData>(TData data)
        {
           
                if (SessionContext.Repository.SaveChanges() > 0)
                    return new ResultModel<TData>(true, 0, "", data);
            return new ResultModel<TData>(false, -2, ErrorMessage.DBNoChanges, data);
        }
        public ResultModel<TData> CheckModelCallback<TData>(Func<TData> callback)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            //参数验证通过
            if (ModelState.IsValid)
            {
                var data = callback.Invoke();
                return SaveChanges(data);
            }
            return new ResultModel<TData>(false) { Code = -1, Message = ModelState.PopErrorMessage() };
        }
        public ResultModel<TData> CheckParameterCallback<TData>(Func<TData> callback, params object[] parameters)
        {
            if (parameters != null)
            {
                bool pass = true;
                foreach (object parameter in parameters)
                {
                    if (
                        (((parameter is int) || (parameter is long)) && Convert.ToInt64(parameter) == 0) ||
                        ((parameter is string) && string.IsNullOrWhiteSpace(parameter.ToString()))
                        )
                    {
                        pass = false;
                        break;
                    }
                }
                if (!pass)
                {
                    return new ResultModel<TData>(false) { Code = -1, Message = ErrorMessage.ArgumentError };
                }
            }
            var data = callback.Invoke();
            return SaveChanges(data);
        }
        #endregion
    }
}