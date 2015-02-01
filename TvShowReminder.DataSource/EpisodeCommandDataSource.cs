using Dapper;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.DataSource
{
    public class EpisodeCommandDataSource : IEpisodeCommandDataSource
    {
        private readonly IDbConnectionHelper _connection;

        public EpisodeCommandDataSource(IDbConnectionHelper connection)
        {
            _connection = connection;
        }

        public void SaveEpisode(Episode episode)
        {
            _connection.Open(c => c.Execute("INSERT INTO Episodes (SubscriptionId, SeasonNumber, EpisodeNumber, Title, AirDate) VALUES (@subscriptionId, @seasonNumber, @episodeNumber, @title, @airDate)", 
                new { subscriptionId = episode.SubscriptionId, seasonNumber = episode.SeasonNumber, episodeNumber = episode.EpisodeNumber, title = episode.Title, airDate = episode.AirDate }));
        }

        public void DeleteEpisode(int episodeId)
        {
            _connection.Open(c => c.Execute("DELETE FROM Episodes WHERE Id = @Id", new { Id = episodeId}));
        }

        public void DeleteAllFromSubscription(int subscriptionId)
        {
            _connection.Open(c => c.Execute("DELETE FROM Episodes WHERE SubscriptionId = @subscriptionId", new { subscriptionId }));
        }
    }
}