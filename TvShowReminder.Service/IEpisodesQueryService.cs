using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;

namespace TvShowReminder.Service
{
    public interface IEpisodesQueryService
    {
        EpisodesToDateResult GetEpisodesUpToDate(EpisodesToDateQuery query);
    }
}