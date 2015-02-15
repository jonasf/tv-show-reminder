using System.Linq;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service
{
    public class EpisodesQueryService : IEpisodesQueryService
    {
        private readonly IEpisodesQueryDataSource _episodesQueryDataSource;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        public EpisodesQueryService(IEpisodesQueryDataSource episodesQueryDataSource,
                                    ISubscriptionQueryDataSource subscriptionQueryDataSource)
        {
            _episodesQueryDataSource = episodesQueryDataSource;
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
        }

        public EpisodesToDateResult GetEpisodesUpToDate(EpisodesToDateQuery query)
        {
            var shows = _subscriptionQueryDataSource.GetAllSubscriptions();
            var episodes = _episodesQueryDataSource.GetToDate(query.ToDate);

            return new EpisodesToDateResult
            {
                Episodes = episodes.Select(e => new EpisodeWithSubscriptionInfoDto
                {
                    Episode = e,
                    Subscription = shows.First(id => id.Id == e.SubscriptionId)
                })
            };
        }
    }
}
