using TvShowReminder.Contracts.Command;

namespace TvShowReminder.Service
{
    public interface IEpisodesCommandService
    {
        void UpdateEpisodeList();
        void RefreshEpisodes(RefreshEpisodesCommand command);
    }
}