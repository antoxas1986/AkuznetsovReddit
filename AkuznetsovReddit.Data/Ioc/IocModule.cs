using AkuznetsovReddit.Data.Context;
using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Repository;
using AkuznetsovReddit.Data.Repository.Interfaces;
using Autofac;

namespace AkuznetsovReddit.Data.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new Core.Ioc.IocModule());
            builder.RegisterType<RedditContext>().As<IRedditContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<RoleRepo>().As<IRoleRepo>();
            builder.RegisterType<UserRepo>().As<IUserRepo>();
            builder.RegisterType<TopicRepo>().As<ITopicRepo>();
            builder.RegisterType<PostsRepo>().As<IPostsRepo>();
        }
    }
}
