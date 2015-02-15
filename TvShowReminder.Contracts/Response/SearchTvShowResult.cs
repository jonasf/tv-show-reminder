using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.Contracts.Response
{
    public class SearchTvShowResult
    {
        public IEnumerable<TvShow> TvShows { get; set; }
    }
}
