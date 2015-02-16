using Autofac;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;
using TvShowReminder.Service;
using TvShowReminder.Service.Command;
using TvShowReminder.Service.Query;
using TvShowReminder.TvRageApi;

namespace TvShowReminder.Startup.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TvRageService>().As<ITvRageService>();
            builder.RegisterType<SubscriptionQueryService>().As<ISubscriptionQueryService>();
            builder.RegisterType<EpisodesCommandService>().As<IEpisodesCommandService>();

            builder.RegisterType<DeleteEpisodesCommandHandler>().As<ICommandHandler<DeleteEpisodesCommand>>();
            builder.RegisterType<AddSubscriptionCommandHandler>().As<ICommandHandler<AddSubscriptionCommand>>();
            builder.RegisterType<EpisodesToDateQueryHandler>().As<IQueryHandler<EpisodesToDateQuery, EpisodesToDateResult>>();
            builder.RegisterType<SearchTvShowQueryHandler>().As<IQueryHandler<SearchTvShowQuery, SearchTvShowResult>>();
            builder.RegisterType<DeleteSubscriptionCommandHandler>().As<ICommandHandler<DeleteSubscriptionCommand>>();
        }
    }
}