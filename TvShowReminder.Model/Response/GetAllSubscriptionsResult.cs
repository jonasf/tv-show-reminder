using System.Collections.Generic;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.Model.Response
{
    public class GetAllSubscriptionsResult
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
