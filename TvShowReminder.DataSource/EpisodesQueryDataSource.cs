using System;
using System.Collections.Generic;
using Dapper;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.DataSource
{
    public class EpisodesQueryDataSource : IEpisodesQueryDataSource
    {
        private readonly IDbConnectionHelper _connection;

        public EpisodesQueryDataSource(IDbConnectionHelper connection)
        {
            _connection = connection;
        }

        public IEnumerable<Episode> GetToDate(DateTime toDate)
        {
            return _connection.Open(c => c.Query<Episode>("SELECT * FROM Episodes WHERE AirDate <= @airDate ORDER BY AirDate DESC", new { airDate = toDate }));
        }
    }
}
