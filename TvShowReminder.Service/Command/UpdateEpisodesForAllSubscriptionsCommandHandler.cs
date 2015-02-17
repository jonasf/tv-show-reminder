using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service.Command
{
    public class UpdateEpisodesForAllSubscriptionsCommandHandler : ICommandHandler<UpdateEpisodesForAllSubscriptionsCommand>
    {
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private readonly IUpdateEpisodesService _updateEpisodesService;

        public UpdateEpisodesForAllSubscriptionsCommandHandler(ISubscriptionQueryDataSource subscriptionQueryDataSource, 
                                                                ISubscriptionCommandDataSource subscriptionCommandDataSource, 
                                                                IUpdateEpisodesService updateEpisodesService)
        {
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
            _subscriptionCommandDataSource = subscriptionCommandDataSource;
            _updateEpisodesService = updateEpisodesService;
        }

        public void Handle(UpdateEpisodesForAllSubscriptionsCommand command)
        {
            var subscriptions = _subscriptionQueryDataSource.GetAllSubscriptions();
            foreach (var subscription in subscriptions)
            {
                var lastAirDate = _updateEpisodesService.UpdateEpisodesForSubscription(subscription);
                if (lastAirDate != null)
                    _subscriptionCommandDataSource.SaveLastAirDate(subscription.Id, lastAirDate.Value.Date);
            }
        }
    }
}
