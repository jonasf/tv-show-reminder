using System.Collections.Generic;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.Models
{
    public class EpisodeListViewModel
    {
        public IEnumerable<EpisodeWithSubscriptionInfoDto> EpisodeList { get; set; }
        public bool HasResults { get; set; }
    }
}