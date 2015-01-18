using System.Collections.Generic;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.Models
{
    public class SubscriptionsListViewModel
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}