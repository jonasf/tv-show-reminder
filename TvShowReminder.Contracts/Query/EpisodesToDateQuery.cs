using System;
using TvShowReminder.Contracts.Response;

namespace TvShowReminder.Contracts.Query
{
    public class EpisodesToDateQuery : IQuery<EpisodesToDateResult>
    {
        public DateTime ToDate { get; set; }
    }
}
