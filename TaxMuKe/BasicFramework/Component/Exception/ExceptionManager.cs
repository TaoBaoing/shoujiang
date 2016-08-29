using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Web.Configuration;

namespace BasicFramework.Component
{
    public class ExceptionManager
    {
        private static readonly Dictionary<string, IExceptionProvider> InstancePool;
        private static readonly object ThisLock = new object();
        private readonly static ExceptionSection Section;

        static ExceptionManager()
        {
            InstancePool = new Dictionary<string, IExceptionProvider>();
            System.Configuration.Configuration configSections;
            try
            {
                configSections = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (ArgumentException e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
#endif
                configSections = WebConfigurationManager.OpenWebConfiguration("~");
            }

            for (var i = 0; i < configSections.Sections.Count; i++)
            {
                var exceptionSection = configSections.Sections[i] as ExceptionSection;
                if (exceptionSection != null)
                {
                    Section = exceptionSection;
                    break;
                }
            }
        }

        public static IExceptionProvider CreateInstance(string eleName)
        {
            if (InstancePool.ContainsKey(eleName))
            {
                return InstancePool[eleName];
            }

            if (Section == null)
            {
                throw new ConfigurationErrorsException("配置文件读取失敗。");
            }

            var ele = Section.Items[eleName];
            if (ele == null)
            {
                throw new ConfigurationErrorsException(string.Format("无效的配置节点\"{0}\"。", eleName));
            }
            var providerType = Type.GetType(ele.Provider);
            if (providerType == null)
            {
                throw new ConfigurationErrorsException(string.Format("无效的加载类型\"{0}\"。", ele.Provider));
            }

            lock (ThisLock)
            {
                if (InstancePool.ContainsKey(eleName))
                {
                    return InstancePool[eleName];
                }

                var instance = Activator.CreateInstance(providerType, ele.BusinessName, ele.DescriptionXml);
                InstancePool.Add(eleName, (IExceptionProvider)instance);

                return InstancePool[eleName];
            }
        }
    }
}
