using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web.Ioc
{
    public class AutofacConfig
    {
        private static IContainer _container;

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new IocModule());
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
        }

        public static T GetContainer<T>()
        {
            return (T)_container;
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
