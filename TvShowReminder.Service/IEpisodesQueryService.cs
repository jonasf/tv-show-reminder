using TvShowReminder.Model.Dto;
using TvShowReminder.Model.Query;

namespace TvShowReminder.Service
{
    public interface IEpisodesQueryService
    {
        EpisodesToDateResult GetEpisodesUpToDate(EpisodesToDateQuery query);
    }
}