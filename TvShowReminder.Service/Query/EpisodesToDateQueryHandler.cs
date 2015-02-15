using System.Linq;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service.Query
{
    public class EpisodesToDateQueryHandler : IQueryHandler<EpisodesToDateQuery, EpisodesToDateResult>
    {
        private readonly IEpisodesQueryDataSource _episodesQueryDataSource;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        public EpisodesToDateQueryHandler(IEpisodesQueryDataSource episodesQueryDataSource, 
                                            ISubscriptionQueryDataSource subscriptionQueryDataSource)
        {
            _episodesQueryDataSource = episodesQueryDataSource;
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
        }

        public EpisodesToDateResult Handle(EpisodesToDateQuery query)
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
