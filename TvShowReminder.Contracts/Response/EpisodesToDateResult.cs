using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.Contracts.Response
{
    public class EpisodesToDateResult
    {
        public IEnumerable<EpisodeWithSubscriptionInfoDto> Episodes { get; set; }
    }
}
