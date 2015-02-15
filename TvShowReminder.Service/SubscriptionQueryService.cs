using System.Collections.Generic;
using System.Linq;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;
using TvShowReminder.DataSource;
using TvShowReminder.TvRageApi;

namespace TvShowReminder.Service
{
    public class SubscriptionQueryService : ISubscriptionQueryService
    {
        private readonly ITvRageService _tvRageService;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly IEpisodesQueryDataSource _episodesQueryDataSource;

        public SubscriptionQueryService(ITvRageService tvRageService,   
                                        ISubscriptionQueryDataSource subscriptionQueryDataSource, 
                                        IEpisodesQueryDataSource episodesQueryDataSource)
        {
            _tvRageService = tvRageService;
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
            _episodesQueryDataSource = episodesQueryDataSource;
        }

        public SearchTvShowResult Search(SearchTvShowQuery searchTvShowQuery)
        {
            var searchResult = _tvRageService.Search(searchTvShowQuery.Query);
            var subscribedShows = _subscriptionQueryDataSource.GetAllSubscriptionIds().ToList();

            var result = searchResult.Select(show => new TvShow
            {
                Id = show.ShowId,
                Name = show.Name,
                Link = show.Link,
                StartedYear = show.Started,
                EndedYear = show.Ended,
                IsSubscribed = CheckIfSubscribed(subscribedShows, show.ShowId)
            });

            return new SearchTvShowResult
            {
                TvShows = result
            };
        }

        public GetAllSubscriptionsWithNextEpisodeResult GetAllWithNextEpisode()
        {
            var subscriptions = _subscriptionQueryDataSource.GetAllSubscriptions();

            var result = new List<SubscriptionWithNextEpisodeDto>();
            foreach (var subscription in subscriptions)
            {
                result.Add(new SubscriptionWithNextEpisodeDto
                {
                    Subscription = subscription,
                    NextEpisode = _episodesQueryDataSource.GetNextEpisode(subscription.Id)
                });
            }

            return new GetAllSubscriptionsWithNextEpisodeResult
            {
                Subscriptions = result
            };
        }

        private bool CheckIfSubscribed(List<int> subscribedShows, int showId)
        {
            return subscribedShows.Contains(showId);
        }
    }
}
