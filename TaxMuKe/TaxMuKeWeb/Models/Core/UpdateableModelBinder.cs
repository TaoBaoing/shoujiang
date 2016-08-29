using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData;
using BasicUPMS.Controllers;

namespace BasicUPMS.Models
{
    /// <summary>
    /// 自动跟踪属性更改，按需更新
    /// </summary>
    public class UpdateableModelBinder : DefaultModelBinder
    {
        List<string> ToBindProperties = new List<string>(); 
        //是否要绑定当前属性            
        bool IsBindTheProperty;
        //当前属性是否绑定完成
        bool IsBindThePropertyComplete;
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            var val = bindingContext.ValueProvider.GetValue(propertyDescriptor.Name);
            if (val != null)
            {
                IsBindTheProperty = true;
                IsBindThePropertyComplete = false;
                //按需进行模型验证
                ToBindProperties.Add(propertyDescriptor.Name);
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
                if (!IsBindThePropertyComplete)
                {
                    //自动审计
                    propertyDescriptor.SetValue(bindingContext.Model, val.RawValue);
                    IsBindThePropertyComplete = true;
                }
                //只针对课程 
//                if (controllerContext.Controller is CourseController && bindingContext.Model is MuKeCourse)
//                {
//                    if (propertyDescriptor.Name == "IsNotSet" || propertyDescriptor.Name == "IsFree" ||
//                        propertyDescriptor.Name == "IsGlod" || propertyDescriptor.Name == "IsWhiteGlod")
//                    {
//                        propertyDescriptor.SetValue(bindingContext.Model, Convert.ToInt32(((string[])val.RawValue)[0]));
//                        IsBindThePropertyComplete = true;
//                    }
//                }

                //首先绑定Id，之后的属性自动跟踪
                if (propertyDescriptor.Name == "Id")
                {
                    SessionContext.Repository.Set(bindingContext.ModelType).Attach(bindingContext.Model);
                }

            }

            IsBindTheProperty = false;
        }


        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = base.BindModel(controllerContext, bindingContext);
            if (!bindingContext.ModelState.IsValid)
            {
                SessionContext.Repository.DetachedAllEntitis();
            }
            return result;
        }

        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            Dictionary<string, bool> startedValid = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

            foreach (ModelValidationResult validationResult in ModelValidator.GetModelValidator(bindingContext.ModelMetadata, controllerContext).Validate(null))
            {
                string subPropertyName = CreateSubPropertyName(bindingContext.ModelName, validationResult.MemberName);
                if (!ToBindProperties.Contains(subPropertyName)) continue;
                if (!startedValid.ContainsKey(subPropertyName))
                {
                    startedValid[subPropertyName] = bindingContext.ModelState.IsValidField(subPropertyName);
                }

                if (startedValid[subPropertyName])
                {
                    bindingContext.ModelState.AddModelError(subPropertyName, validationResult.Message);
                }
            }
            
        }

        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            object value = propertyBinder.BindModel(controllerContext, bindingContext);
            //解决可空属性的验证问题
            if (value is string)
            {
                if (string.IsNullOrWhiteSpace((string)value))
                {
                    value = null;
                }
            }
            IsBindThePropertyComplete = true;
            return value;

        }

        protected override bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor, object value)
        {
            if (IsBindTheProperty)
                return true;
            return false;
        }

    }

}