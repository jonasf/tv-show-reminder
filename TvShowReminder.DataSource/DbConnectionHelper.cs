using System;
using System.Data;
using System.Data.SqlClient;

namespace TvShowReminder.DataSource
{
    public class DbConnectionHelper : IDbConnectionHelper
    {
        private readonly string _connectionString;

        public DbConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Open(Action<IDbConnection> action)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                action(connection);
            }
        }

        public T Open<T>(Func<IDbConnection, T> action)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return action(connection);
            }
        }
    }
}
