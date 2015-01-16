using System.Collections.Generic;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.Model.Response
{
    public class SearchTvShowResult
    {
        public IEnumerable<TvShow> TvShows { get; set; }
    }
}
