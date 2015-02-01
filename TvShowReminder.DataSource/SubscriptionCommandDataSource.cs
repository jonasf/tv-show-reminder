using System;
using Dapper;

namespace TvShowReminder.DataSource
{
    public class SubscriptionCommandDataSource : ISubscriptionCommandDataSource
    {
        private readonly IDbConnectionHelper _connection;

        public SubscriptionCommandDataSource(IDbConnectionHelper connection)
        {
            _connection = connection;
        }

        public void Insert(int showId, string showName, DateTime lastAirDate)
        {
            _connection.Open(c => c.Execute("INSERT INTO Subscription (TvShowId, TvShowName, LastAirDate) VALUES (@tvShowId, @tvShowName, @lastAirDate)", new { tvShowId = showId, tvShowName = showName, lastAirDate = lastAirDate }));
        }

        public void SaveLastAirDate(int subscriptionId, DateTime lastAirDate)
        {
            _connection.Open(c => c.Execute("UPDATE Subscription SET LastAirDate=@lastAirDate WHERE id=@subscriptionId", new {subscriptionId, lastAirDate}));
        }

        public void Delete(int subscriptionId)
        {
            _connection.Open(c => c.Execute("DELETE FROM Subscription WHERE id=@subscriptionId", new {subscriptionId}));
        }
    }
}
