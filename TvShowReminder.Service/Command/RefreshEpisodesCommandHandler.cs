using System;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service.Command
{
    public class RefreshEpisodesCommandHandler : ICommandHandler<RefreshEpisodesCommand>
    {
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;
        private readonly IUpdateEpisodesService _updateEpisodesService;

        public RefreshEpisodesCommandHandler(ISubscriptionQueryDataSource subscriptionQueryDataSource, 
                                                ISubscriptionCommandDataSource subscriptionCommandDataSource, 
                                                IEpisodeCommandDataSource episodeCommandDataSource, 
                                                IUpdateEpisodesService updateEpisodesService)
        {
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
            _subscriptionCommandDataSource = subscriptionCommandDataSource;
            _episodeCommandDataSource = episodeCommandDataSource;
            _updateEpisodesService = updateEpisodesService;
        }

        public void Handle(RefreshEpisodesCommand command)
        {
            var subscription = _subscriptionQueryDataSource.GetSubscription(command.SubscriptionId);
            subscription.LastAirDate = DateTime.Now.Date;

            _episodeCommandDataSource.DeleteAllFromSubscription(command.SubscriptionId);

            var lastAirDate = _updateEpisodesService.UpdateEpisodesForSubscription(subscription);
            if (lastAirDate != null)
                _subscriptionCommandDataSource.SaveLastAirDate(subscription.Id, lastAirDate.Value.Date);
        }
    }
}
