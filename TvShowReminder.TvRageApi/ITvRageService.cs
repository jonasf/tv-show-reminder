using System.Collections.Generic;
using TvShowReminder.TvRageApi.Domain;

namespace TvShowReminder.TvRageApi
{
    public interface ITvRageService
    {
        IEnumerable<Show> Search(string query);
        EpisodeList GetEpisodes(int showId);
    }
}