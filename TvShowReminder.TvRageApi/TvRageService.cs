using System.Collections.Generic;

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
    }
}
