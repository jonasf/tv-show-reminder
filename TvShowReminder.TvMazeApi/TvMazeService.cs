using System;
using System.Collections.Generic;
using TvShowReminder.TvMazeApi.Domain;
using TvShowReminder.TvMazeApi.Utilities;

namespace TvShowReminder.TvMazeApi
{
    public class TvMazeService : ITvMazeService
    {
        private readonly HttpClient _httpClient;

        public TvMazeService()
        {
            _httpClient = new HttpClient();
        }

        public IEnumerable<TvMazeShow> Search(string query)
        {
            var rawResponse = _httpClient.Get(TvMazeApiUrls.CreateSearchUrl(query));
            return TvMazeMapper.MapShows(rawResponse);
        }

        public IEnumerable<TvMazeEpisode> GetEpisodes(int showId)
        {
            var rawResponse = _httpClient.Get(TvMazeApiUrls.CreateEpisodeListUrl(showId));
            return TvMazeMapper.MapEpisodes(rawResponse);
        }
    }
}
