using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;

namespace TvShowReminder.Service
{
    public interface IEpisodesQueryService
    {
        EpisodesToDateResult GetEpisodesUpToDate(EpisodesToDateQuery query);
    }
}