using System;

namespace TvShowReminder.TvRageApi.Domain
{
    public class TvRageEpisode
    {
        public int EpNum { get; set; }
        public int SeasonNum { get; set; }
        public string ProdNum { get; set; }
        public DateTime AirDate { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
    }
}
