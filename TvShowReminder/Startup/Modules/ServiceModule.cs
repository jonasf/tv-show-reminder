using Autofac;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.Service;
using TvShowReminder.Service.Command;
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
            builder.RegisterType<EpisodesQueryService>().As<IEpisodesQueryService>();

            builder.RegisterType<DeleteEpisodesCommandHandler>().As<ICommandHandler<DeleteEpisodesCommand>>();
            builder.RegisterType<AddSubscriptionCommandHandler>().As<ICommandHandler<AddSubscriptionCommand>>();
        }
    }
}