using System;
using NSubstitute;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;
using TvShowReminder.Service;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class SubscriptionCommandServiceTests
    {
        private readonly SubscriptionCommandService _subscriptionCommandService;

        private ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private IEpisodeCommandDataSource _episodeCommandDataSource;

        public SubscriptionCommandServiceTests()
        {
            _subscriptionCommandDataSource = Substitute.For<ISubscriptionCommandDataSource>();
            _episodeCommandDataSource = Substitute.For<IEpisodeCommandDataSource>();

            _subscriptionCommandService = new SubscriptionCommandService(_subscriptionCommandDataSource, _episodeCommandDataSource);
        }

        [Fact]
        public void Should_delete_subscription()
        {
            const int subscriptionId = 1;
            var command = new DeleteSubscriptionCommand { SubscriptionId = subscriptionId };

            _subscriptionCommandService.DeleteSubscription(command);

            _episodeCommandDataSource.Received(1).DeleteAllFromSubscription(subscriptionId);
            _subscriptionCommandDataSource.Received(1).Delete(subscriptionId);
        }
    }
}
