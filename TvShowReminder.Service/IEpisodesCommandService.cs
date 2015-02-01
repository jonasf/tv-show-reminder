using TvShowReminder.Model.Command;

namespace TvShowReminder.Service
{
    public interface IEpisodesCommandService
    {
        void UpdateEpisodeList();
        void DeleteEpisodes(int[] episodeIds);
        void RefreshEpisodes(RefreshEpisodesCommand command);
    }
}