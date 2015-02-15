using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using TvShowReminder.Contracts.Dto;

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

        public Episode GetNextEpisode(int subscriptionId)
        {
            return _connection.Open(c => c.Query<Episode>("SELECT top 1 Id, SubscriptionId,SeasonNumber, EpisodeNumber, Title, AirDate from Episodes where SubscriptionId = @subscriptionId order by AirDate ASC", new {subscriptionId })).FirstOrDefault();
        }
    }
}
