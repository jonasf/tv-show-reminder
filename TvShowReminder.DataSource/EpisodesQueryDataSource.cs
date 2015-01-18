using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.DataSource
{
    public class EpisodesQueryDataSource : IEpisodesQueryDataSource
    {
        private readonly IDbConnection _connection;

        public EpisodesQueryDataSource(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Episode> GetToDate(DateTime toDate)
        {
            return _connection.Query<Episode>("SELECT * FROM Episodes WHERE AirDate <= @airDate ORDER BY AirDate DESC", new { airDate = toDate });
        }
    }
}
