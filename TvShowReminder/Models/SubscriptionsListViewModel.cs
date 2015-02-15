using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.Models
{
    public class SubscriptionsListViewModel
    {
        public IEnumerable<SubscriptionWithNextEpisodeDto> Subscriptions { get; set; }
    }
}