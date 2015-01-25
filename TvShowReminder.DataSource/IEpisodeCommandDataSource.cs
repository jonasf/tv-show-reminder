using TvShowReminder.Model.Dto;

namespace TvShowReminder.DataSource
{
    public interface IEpisodeCommandDataSource
    {
        void SaveEpisode(Episode episode);
        void DeleteEpisode(int episodeId);
        void DeleteAllFromSubscription(int subscriptionId);
    }
}
