using System.Collections.Generic;
using System.Data;
using Dapper;

namespace TvShowReminder.DataSource
{
    public class SubscriptionQueryDataSource : ISubscriptionQueryDataSource
    {
        private readonly IDbConnection _connection;

        public SubscriptionQueryDataSource(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<int> GetAllSubscriptionIds()
        {
            return _connection.Query<int>("SELECT TvShowId FROM Subscription");
        }
    }
}
