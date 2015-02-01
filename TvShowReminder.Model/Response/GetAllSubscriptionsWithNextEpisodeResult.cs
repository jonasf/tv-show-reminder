using System.Collections.Generic;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.Model.Response
{
    public class GetAllSubscriptionsWithNextEpisodeResult
    {
        public IEnumerable<SubscriptionWithNextEpisodeDto> Subscriptions { get; set; }
    }
}
