using System;
using System.Collections.Generic;
using TvShowReminder.Model.Dto;

namespace TvShowReminder.DataSource
{
    public interface IEpisodesQueryDataSource
    {
        IEnumerable<Episode> GetToDate(DateTime toDate);
    }
}