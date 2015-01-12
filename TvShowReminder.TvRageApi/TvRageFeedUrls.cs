﻿namespace TvShowReminder.TvRageApi
{
    public static class TvRageFeedUrls
    {
        public const string FeedBaseUrl = "http://services.tvrage.com/feeds/";

        public static string CreateSearchUrl(string query)
        {
            return string.Format("{0}/search.php?show={1}", FeedBaseUrl, query);
        }

        public static string CreateEpisodeListUrl(int showId)
        {
            return string.Format("{0}/episode_list.php?sid={1}", FeedBaseUrl, showId);
        }
    }
}