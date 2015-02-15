using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.Contracts.Response
{
    public class GetAllSubscriptionsWithNextEpisodeResult
    {
        public IEnumerable<SubscriptionWithNextEpisodeDto> Subscriptions { get; set; }
    }
}
