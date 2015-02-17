using Autofac;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;
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
            builder.RegisterType<UpdateEpisodesService>().As<IUpdateEpisodesService>();

            builder.RegisterType<DeleteEpisodesCommandHandler>().As<ICommandHandler<DeleteEpisodesCommand>>();
            builder.RegisterType<AddSubscriptionCommandHandler>().As<ICommandHandler<AddSubscriptionCommand>>();
            builder.RegisterType<EpisodesToDateQueryHandler>().As<IQueryHandler<EpisodesToDateQuery, EpisodesToDateResult>>();
            builder.RegisterType<SearchTvShowQueryHandler>().As<IQueryHandler<SearchTvShowQuery, SearchTvShowResult>>();
            builder.RegisterType<DeleteSubscriptionCommandHandler>().As<ICommandHandler<DeleteSubscriptionCommand>>();
            builder.RegisterType<AllSubscriptionsWithNextEpisodeQueryHandler>()
                .As<IQueryHandler<AllSubscriptionsWithNextEpisodeQuery, AllSubscriptionsWithNextEpisodeResult>>();
            builder.RegisterType<RefreshEpisodesCommandHandler>().As<ICommandHandler<RefreshEpisodesCommand>>();
            builder.RegisterType<UpdateEpisodesForAllSubscriptionsCommandHandler>().As<ICommandHandler<UpdateEpisodesForAllSubscriptionsCommand>>();
        }
    }
}