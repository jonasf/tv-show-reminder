using System.Collections.Generic;
using System.Linq;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;
using TvShowReminder.DataSource;
using TvShowReminder.TvRageApi;

namespace TvShowReminder.Service.Query
{
    public class SearchTvShowQueryHandler : IQueryHandler<SearchTvShowQuery, SearchTvShowResult>
    {
        private readonly ITvRageService _tvRageService;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        public SearchTvShowQueryHandler(ITvRageService tvRageService, 
                                            ISubscriptionQueryDataSource subscriptionQueryDataSource)
        {
            _tvRageService = tvRageService;
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
        }

        public SearchTvShowResult Handle(SearchTvShowQuery query)
        {
            var searchResult = _tvRageService.Search(query.Query);
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
