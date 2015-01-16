using System.Linq;
using TvShowReminder.Model.Dto;
using TvShowReminder.Model.Query;
using TvShowReminder.Model.Response;
using TvShowReminder.TvRageApi;

namespace TvShowReminder.Service
{
    public class SubscriptionQueryService : ISubscriptionQueryService
    {
        private readonly ITvRageService _tvRageService;

        public SubscriptionQueryService(ITvRageService tvRageService)
        {
            _tvRageService = tvRageService;
        }

        public SearchTvShowResult Search(SearchTvShowQuery searchTvShowQuery)
        {
            var searchResult = _tvRageService.Search(searchTvShowQuery.Query);

            var result = searchResult.Select(show => new TvShow
            {
                Id = show.ShowId,
                Name = show.Name,
                Link = show.Link,
                StartedYear = show.Started,
                EndedYear = show.Ended
            });

            return new SearchTvShowResult
            {
                TvShows = result
            };
        }
    }
}
