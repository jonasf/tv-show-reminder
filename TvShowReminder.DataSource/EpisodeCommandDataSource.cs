using System.Data;
using Dapper;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.DataSource
{
    public class EpisodeCommandDataSource : IEpisodeCommandDataSource
    {
        private readonly IDbConnection _connection;

        public EpisodeCommandDataSource(IDbConnection connection)
        {
            _connection = connection;
        }

        public void SaveEpisode(Episode episode)
        {
            _connection.Execute("INSERT INTO Episodes (SubscriptionId, SeasonNumber, EpisodeNumber, Title, AirDate) VALUES (@subscriptionId, @seasonNumber, @episodeNumber, @title, @airDate)", 
                new { subscriptionId = episode.SubscriptionId, seasonNumber = episode.SeasonNumber, episodeNumber = episode.EpisodeNumber, title = episode.Title, airDate = episode.AirDate });
        }
    }
}