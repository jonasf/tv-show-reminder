using System;
using TvShowReminder.Contracts.Dto;

namespace TvShowReminder.Service.Command
{
    public interface IUpdateEpisodesService
    {
        DateTime? UpdateEpisodesForSubscription(Subscription subscription);
    }
}