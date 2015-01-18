using Autofac;
using TvShowReminder.Service;
using TvShowReminder.TvRageApi;

namespace TvShowReminder.Startup.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TvRageService>().As<ITvRageService>();
            builder.RegisterType<SubscriptionQueryService>().As<ISubscriptionQueryService>();
            builder.RegisterType<SubscriptionCommandService>().As<ISubscriptionCommandService>();
            builder.RegisterType<EpisodesCommandService>().As<IEpisodesCommandService>();
        }
    }
}