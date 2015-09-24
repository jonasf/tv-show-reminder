using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TvShowReminder.TvMazeApi.Domain;

namespace TvShowReminder.TvMazeApi.Utilities
{
    public static class TvMazeMapper
    {
        public static IEnumerable<TvMazeShow> MapShows(string data)
        {
            var deserializedShows = JsonConvert.DeserializeObject<List<TvMazeShowWrapper>>(data);
            return deserializedShows.Select(s => s.Show);
        }

        public static IEnumerable<TvMazeEpisode> MapEpisodes(string data)
        {
            return JsonConvert.DeserializeObject<List<TvMazeEpisode>>(data);
        }
    }
}
