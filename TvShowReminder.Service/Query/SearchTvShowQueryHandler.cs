using System.Collections.Generic;
using System.Linq;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;
using TvShowReminder.DataSource;
using TvShowReminder.TvMazeApi;

namespace TvShowReminder.Service.Query
{
    public class SearchTvShowQueryHandler : IQueryHandler<SearchTvShowQuery, SearchTvShowResult>
    {
        private readonly ITvMazeService _tvMazeService;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        public SearchTvShowQueryHandler(ITvMazeService tvMazeService, 
                                            ISubscriptionQueryDataSource subscriptionQueryDataSource)
        {
            _tvMazeService = tvMazeService;
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
        }

        public SearchTvShowResult Handle(SearchTvShowQuery query)
        {
            var searchResult = _tvMazeService.Search(query.Query);
            var subscribedShows = _subscriptionQueryDataSource.GetAllSubscriptionIds().ToList();

            var result = searchResult.Select(show => new TvShow
            {
                Id = show.Id,
                Name = show.Name,
                Link = show.Url,
                StartedYear = show.Premiered.Year,
                ImageUrl = show.Image.Medium,
                IsSubscribed = CheckIfSubscribed(subscribedShows, show.Id)
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
