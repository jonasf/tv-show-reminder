using System.Collections.Generic;
using System.Linq;
using Dapper;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.DataSource
{
    public class SubscriptionQueryDataSource : ISubscriptionQueryDataSource
    {
        private readonly IDbConnectionHelper _connection;

        public SubscriptionQueryDataSource(IDbConnectionHelper connection)
        {
            _connection = connection;
        }

        public IEnumerable<int> GetAllSubscriptionIds()
        {
            return _connection.Open(c => c.Query<int>("SELECT TvShowId FROM Subscription"));
        }

        public IEnumerable<Subscription> GetAllSubscriptions()
        {
            return _connection.Open(c => c.Query<Subscription>("SELECT Id, TvShowId, TvShowName, LastAirDate FROM Subscription"));
        }

        public Subscription GetSubscription(int subscriptionId)
        {
            return _connection.Open(c => c.Query<Subscription>("SELECT Id, TvShowId, TvShowName, LastAirDate FROM Subscription WHERE Id=@id", new { id = subscriptionId })).FirstOrDefault();
        }
    }
}
