using System.Collections.Generic;
using TvShowReminder.TvRageApi.Domain;
using TvShowReminder.TvRageApi.Utilities;

namespace TvShowReminder.TvRageApi
{
    public class TvRageService
    {
        private readonly HttpClient _httpClient;

        public TvRageService()
        {
            _httpClient = new HttpClient();    
        }

        public IEnumerable<Show> Search(string query)
        {
            var rawResponse = _httpClient.Get(TvRageFeedUrls.CreateSearchUrl(query));    
            return SearchResultParser.Parse(rawResponse);
        }

        public EpisodeList GetEpisodes(int showId)
        {
            var rawResponse = _httpClient.Get(TvRageFeedUrls.CreateEpisodeListUrl(showId));
            return EpisodeListParser.Parse(rawResponse);
        }
    }
}
