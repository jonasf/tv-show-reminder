using System.Collections.Generic;

namespace TvShowReminder.TvRageApi.Domain
{
    public class TvRageEpisodeList
    {
        public IEnumerable<TvRageEpisode> Episodes { get; set; }
        public IEnumerable<TvRageSpecialEpisode> SpecialEpisodes { get; set; }
    }
}