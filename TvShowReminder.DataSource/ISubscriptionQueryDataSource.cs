using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.DataSource
{
    public interface ISubscriptionQueryDataSource
    {
        IEnumerable<int> GetAllSubscriptionIds();
        IEnumerable<Subscription> GetAllSubscriptions();
        Subscription GetSubscription(int subscriptionId);
    }
}