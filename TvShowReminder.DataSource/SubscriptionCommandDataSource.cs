using System;
using System.Data;
using Dapper;

namespace TvShowReminder.DataSource
{
    public class SubscriptionCommandDataSource : ISubscriptionCommandDataSource
    {
        private readonly IDbConnection _connection;

        public SubscriptionCommandDataSource(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Insert(int showId, string showName, DateTime lastAirDate)
        {
            _connection.Execute("INSERT INTO Subscription (TvShowId, TvShowName, LastAirDate) VALUES (@tvShowId, @tvShowName, @lastAirDate)", new { tvShowId = showId, tvShowName = showName, lastAirDate = lastAirDate });
        }
    }
}
