using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using BasicUPMS.Models;
using FluentValidation.Mvc;
using System.Web.Security;
using BasicData;
using BasicCommon;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace BasicUPMS
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected ILog log = LogManager.GetLogger("log4net");
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Log4net配置
            log4net.Config.XmlConfigurator.Configure();

            //Debug模式下输出SQL日志以便诊断
            if (UPMSConfig.Debug)
            {
                DbInterception.Add(new DbCommandLoggingInterceptor());
            }

            //模型验证
            FluentValidationModelValidatorProvider.Configure();

            //只保留Razor引擎
            ViewEngines.Engines.Clear(); 
            ViewEngines.Engines.Add(new RazorViewEngine());

            //自动审计
            ValueProviderFactories.Factories.Add(new CustomValueProviderFactory()); 

            //不检查EF数据模型
            Database.SetInitializer<BasicData.UcfUniversityContext>(null);

            //初始化七牛
            Qiniu.Conf.Config.Init();


            //http://www.cnblogs.com/dudu/p/entity-framework-warm-up.html
            //预生成映射视图
            using (var dbcontext = new UcfUniversityContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
                //对程序中定义的所有DbContext逐一进行这个操作
            }
        }

        public MvcApplication()
        {
            
            //安全主体设置
            AuthenticateRequest += MvcApplication_AuthenticateRequest;
            EndRequest += MvcApplication_EndRequest;
            BeginRequest += Application_BeginRequest;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var url = Request.Url.AbsoluteUri;
//            log.Info("BeginRequest-----"+ url);
            //stopWatch = new Stopwatch();
            // stopWatch.Start();
        }
        /// <summary>
        /// 释放DB上下文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MvcApplication_EndRequest(object sender, EventArgs e)
        {
            var entityContext = HttpContext.Current.Items["dbContext"] as UcfUniversityContext;
            if (entityContext != null)
                entityContext.Dispose();
        }
        public void MvcApplication_AuthenticateRequest(object sender, EventArgs e)
        {
           
            //忽略Assets文件夹
            if (Request.RawUrl.StartsWith("/Assets/", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            if (Request.RawUrl.StartsWith("/favicon.ico", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (null == authCookie)
            {
                return;
            }
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("解密失败，未能授权。详细信息：{0}", ex.ToString()));
                return;
            }
            if (null == authTicket)
            {
                return;
            }
            long roleId = int.Parse(authTicket.UserData.Split('|')[0]);
            FormsIdentity identity = new FormsIdentity(authTicket);
            identity.Label = authTicket.UserData.Split('|')[1];
            UPMSPrincipal principal = new UPMSPrincipal(identity, roleId);
            Context.User = principal;
        }


    }
}