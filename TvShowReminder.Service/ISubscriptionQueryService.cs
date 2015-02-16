using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;

namespace TvShowReminder.Service
{
    public interface ISubscriptionQueryService
    {
        AllSubscriptionsWithNextEpisodeResult GetAllWithNextEpisode();
    }
}