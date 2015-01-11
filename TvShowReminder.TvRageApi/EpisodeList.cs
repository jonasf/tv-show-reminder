using System.Collections.Generic;

namespace TvShowReminder.TvRageApi
{
    public class EpisodeList
    {
        public IEnumerable<Episode> Episodes { get; set; }
        public IEnumerable<SpecialEpisode> SpecialEpisodes { get; set; }
    }
}