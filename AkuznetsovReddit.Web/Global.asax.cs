using AkuznetsovReddit.Web.Ioc;
using AkuznetsovReddit.Web.Mappers;
using Elmah.Contrib.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace AkuznetsovReddit.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Error()
        {
            var error = Server.GetLastError();
        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

            AutoMapperConfig.Execute();
            AutofacConfig.Configure();

            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
