using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.Models
{
    public class SearchViewModel
    {
        public IEnumerable<TvShow> TvShows { get; set; }
        public int SearchHits { get; set; }
        public bool HasSearch { get; set; }
        public string SearchWords { get; set; }
    }
}