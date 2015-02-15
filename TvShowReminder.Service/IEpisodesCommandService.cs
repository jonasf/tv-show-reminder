using TvShowReminder.Contracts.Command;

namespace TvShowReminder.Service
{
    public interface IEpisodesCommandService
    {
        void UpdateEpisodeList();
        void DeleteEpisodes(int[] episodeIds);
        void RefreshEpisodes(RefreshEpisodesCommand command);
    }
}