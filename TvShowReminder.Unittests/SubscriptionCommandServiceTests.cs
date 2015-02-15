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
        public void Should_add_subscription()
        {
            const int showId = 1;
            const string showName = "The awesome show";
            var command = new AddSubscriptionCommand {ShowId = showId, ShowName = showName};

            _subscriptionCommandService.AddSubscription(command);

            _subscriptionCommandDataSource
                .Received(1)
                .Insert(showId, 
                        showName,
                        Arg.Is<DateTime>(date => date > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0) && date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 59)));
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
