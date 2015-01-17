using System.Collections.Generic;

namespace TvShowReminder.DataSource
{
    public interface ISubscriptionQueryDataSource
    {
        IEnumerable<int> GetAllSubscriptionIds();
    }
}