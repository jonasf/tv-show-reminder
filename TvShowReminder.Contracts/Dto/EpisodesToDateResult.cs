using System.Collections.Generic;

namespace TvShowReminder.Contracts.Dto
{
    public class EpisodesToDateResult
    {
        public IEnumerable<EpisodeWithSubscriptionInfoDto> Episodes { get; set; }
    }
}
