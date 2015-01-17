using System.Collections.Generic;
using System.Linq;
using TvShowReminder.DataSource;
using TvShowReminder.Model.Dto;
using TvShowReminder.Model.Query;
using TvShowReminder.Model.Response;
using TvShowReminder.TvRageApi;

namespace TvShowReminder.Service
{
    public class SubscriptionQueryService : ISubscriptionQueryService
    {
        private readonly ITvRageService _tvRageService;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        public SubscriptionQueryService(ITvRageService tvRageService, 
                                        ISubscriptionQueryDataSource subscriptionQueryDataSource)
        {
            _tvRageService = tvRageService;
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
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

        private bool CheckIfSubscribed(List<int> subscribedShows, int showId)
        {
            return subscribedShows.Contains(showId);
        }
    }
}
