namespace TvShowReminder.TvMazeApi
{
    public class TvMazeApiUrls
    {
        public const string ApiBaseUrl = "http://api.tvmaze.com";

        public static string CreateSearchUrl(string query)
        {
            return string.Format("{0}/search/shows?q={1}", ApiBaseUrl, query);
        }

        public static string CreateEpisodeListUrl(int showId)
        {
            return string.Format("{0}/shows/{1}/episodes?specials=1", ApiBaseUrl, showId);
        }
    }
}
