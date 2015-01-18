using System.Collections.Generic;
using TvShowReminder.Model.Response;

namespace TvShowReminder.Model.Dto
{
    public class EpisodesToDateResult
    {
        public IEnumerable<EpisodeWithSubscriptionInfoDto> Episodes { get; set; }
    }
}
