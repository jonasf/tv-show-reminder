namespace TvShowReminder.Service
{
    public interface IEpisodesCommandService
    {
        void UpdateEpisodeList();
        void DeleteEpisodes(int[] episodeIds);
    }
}