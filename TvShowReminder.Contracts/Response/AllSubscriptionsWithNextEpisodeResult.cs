using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.Contracts.Response
{
    public class AllSubscriptionsWithNextEpisodeResult
    {
        public IEnumerable<SubscriptionWithNextEpisodeDto> Subscriptions { get; set; }
    }
}
