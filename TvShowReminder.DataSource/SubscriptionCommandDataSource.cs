using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace TvShowReminder.DataSource
{
    public class SubscriptionCommandDataSource : ISubscriptionCommandDataSource
    {
        private readonly IDbConnection _connection;

        public SubscriptionCommandDataSource(string connectionstring)
        {
            _connection = new SqlConnection(connectionstring);
        }

        public void Insert(int showId, string showName, DateTime lastAirDate)
        {
            _connection.Execute("INSERT INTO Subscription (TvShowId, TvShowName, LastAirDate) VALUES (@tvShowId, @tvShowName, @lastAirDate)", new { tvShowId = showId, tvShowName = showName, lastAirDate = lastAirDate });
        }
    }
}
