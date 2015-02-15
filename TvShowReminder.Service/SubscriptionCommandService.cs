using System;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service
{
    public class SubscriptionCommandService : ISubscriptionCommandService
    {
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;

        public SubscriptionCommandService(ISubscriptionCommandDataSource subscriptionCommandDataSource,
                                          IEpisodeCommandDataSource episodeCommandDataSource)
        {
            _subscriptionCommandDataSource = subscriptionCommandDataSource;
            _episodeCommandDataSource = episodeCommandDataSource;
        }

        public void DeleteSubscription(DeleteSubscriptionCommand command)
        {
            _episodeCommandDataSource.DeleteAllFromSubscription(command.SubscriptionId);
            _subscriptionCommandDataSource.Delete(command.SubscriptionId);
        }
    }
}
