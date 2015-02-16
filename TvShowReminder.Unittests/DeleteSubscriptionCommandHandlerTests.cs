using NSubstitute;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Command;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class DeleteSubscriptionCommandHandlerTests
    {
        private DeleteSubscriptionCommandHandler _handler;

        private ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private IEpisodeCommandDataSource _episodeCommandDataSource;

        public DeleteSubscriptionCommandHandlerTests()
        {
            _subscriptionCommandDataSource = Substitute.For<ISubscriptionCommandDataSource>();
            _episodeCommandDataSource = Substitute.For<IEpisodeCommandDataSource>();
            _handler = new DeleteSubscriptionCommandHandler(_subscriptionCommandDataSource, _episodeCommandDataSource);
        }

        [Fact]
        public void Should_delete_subscription()
        {
            const int subscriptionId = 1;
            var command = new DeleteSubscriptionCommand { SubscriptionId = subscriptionId };

            _handler.Handle(command);

            _episodeCommandDataSource.Received(1).DeleteAllFromSubscription(subscriptionId);
            _subscriptionCommandDataSource.Received(1).Delete(subscriptionId);
        }
    }
}
