using TvShowReminder.Model.Query;
using TvShowReminder.Model.Response;

namespace TvShowReminder.Service
{
    public interface ISubscriptionQueryService
    {
        SearchTvShowResult Search(SearchTvShowQuery searchTvShowQuery);
        GetAllSubscriptionsWithNextEpisodeResult GetAllWithNextEpisode();
    }
}