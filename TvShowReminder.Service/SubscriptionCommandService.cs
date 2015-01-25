using System;
using TvShowReminder.DataSource;
using TvShowReminder.Model.Command;

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

        public void AddSubscription(AddSubscriptionCommand command)
        {
            DateTime defaultLastAirDate = DateTime.Now;
            _subscriptionCommandDataSource.Insert(command.ShowId, command.ShowName, defaultLastAirDate);
        }

        public void DeleteSubscription(DeleteSubscriptionCommand command)
        {
            _episodeCommandDataSource.DeleteAllFromSubscription(command.SubscriptionId);
            _subscriptionCommandDataSource.Delete(command.SubscriptionId);
        }
    }
}
