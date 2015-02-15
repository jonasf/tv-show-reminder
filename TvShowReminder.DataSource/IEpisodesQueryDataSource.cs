using System;
using System.Collections.Generic;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.DataSource
{
    public interface IEpisodesQueryDataSource
    {
        IEnumerable<Episode> GetToDate(DateTime toDate);
        Episode GetNextEpisode(int subscriptionId);
    }
}