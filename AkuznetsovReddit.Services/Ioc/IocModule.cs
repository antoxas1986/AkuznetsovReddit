using AkuznetsovReddit.Service;
using AkuznetsovReddit.Services.Interfaces;
using Autofac;

namespace AkuznetsovReddit.Services.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new Data.Ioc.IocModule());
            builder.RegisterType<RegisterService>().As<IRegisterService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<LoginService>().As<ILoginService>();
            builder.RegisterType<TopicService>().As<ITopicService>();
            builder.RegisterType<PostsService>().As<IPostsService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<CryptoService>().As<ICryptoService>();
        }
    }
}
