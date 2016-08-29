using System;
using System.IO;
using System.Linq;

namespace BasicFramework.Component
{
    public class ExceptionProvider : IExceptionProvider
    {
        protected readonly ExceptionDescription ExceptionDescription;
        protected string BusinessName;

        public ExceptionProvider(string businessName, string descriptionXmlPath)
        {
            if (Path.IsPathRooted(descriptionXmlPath) == false)
            {
                descriptionXmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, descriptionXmlPath);
            }

            ExceptionDescription = new ExceptionDescription(descriptionXmlPath);
            BusinessName = string.IsNullOrEmpty(businessName) ? "Common" : businessName;
        }

        public virtual BusinessException Throw(int exceptionId, params object[] args)
        {
            var result = ExceptionDescription.GetDescriptionItem(BusinessName, exceptionId);
            var formatArgs = args != null ? args.Select(d => d ?? "null").ToArray() : new string[0];

            return result == null
                       ? new BusinessException
                             {
                                 ExceptionCode = -255,
                                 ExceptionMessage =
                                     string.Format("未能获取异常分类\"{0}\"的\"{1}\"描述信息。", BusinessName, exceptionId)
                             }
                       : new BusinessException
                             {
                                 ExceptionCode = result.Id,
                                 ExceptionMessage = formatArgs.Length > 0 ? string.Format(result.Message, formatArgs) : result.Message
                             };
        }
    }
}
