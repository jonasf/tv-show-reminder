using TvShowReminder.Contracts.Response;

namespace TvShowReminder.Contracts.Query
{
    public class SearchTvShowQuery : IQuery<SearchTvShowResult>
    {
        public string Query { get; set; }
    }
}
