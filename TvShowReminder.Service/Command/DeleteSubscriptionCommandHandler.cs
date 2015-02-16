using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service.Command
{
    public class DeleteSubscriptionCommandHandler : ICommandHandler<DeleteSubscriptionCommand>
    {
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;

        public DeleteSubscriptionCommandHandler(ISubscriptionCommandDataSource subscriptionCommandDataSource, 
                                                    IEpisodeCommandDataSource episodeCommandDataSource)
        {
            _subscriptionCommandDataSource = subscriptionCommandDataSource;
            _episodeCommandDataSource = episodeCommandDataSource;
        }

        public void Handle(DeleteSubscriptionCommand command)
        {
            _episodeCommandDataSource.DeleteAllFromSubscription(command.SubscriptionId);
            _subscriptionCommandDataSource.Delete(command.SubscriptionId);
        }
    }
}
