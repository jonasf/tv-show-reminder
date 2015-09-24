using System.Collections.Generic;
using TvShowReminder.TvMazeApi.Domain;

namespace TvShowReminder.TvMazeApi
{
    public interface ITvMazeService
    {
        IEnumerable<TvMazeShow> Search(string query);
        IEnumerable<TvMazeEpisode> GetEpisodes(int showId);
    }
}