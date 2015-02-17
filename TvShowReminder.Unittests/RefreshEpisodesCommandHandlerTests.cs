using System;
using NSubstitute;
using TvShowReminder.Contracts.Command;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Command;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class RefreshEpisodesCommandHandlerTests
    {
        private readonly RefreshEpisodesCommandHandler _handler;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private readonly IUpdateEpisodesService _updateEpisodesService;
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;

        private readonly Subscription _subscription;

        public RefreshEpisodesCommandHandlerTests()
        {
            _subscriptionQueryDataSource = Substitute.For<ISubscriptionQueryDataSource>();
            _subscriptionCommandDataSource = Substitute.For<ISubscriptionCommandDataSource>();
            _episodeCommandDataSource = Substitute.For<IEpisodeCommandDataSource>();
            _updateEpisodesService = Substitute.For<IUpdateEpisodesService>();

            _handler = new RefreshEpisodesCommandHandler(_subscriptionQueryDataSource, _subscriptionCommandDataSource, _episodeCommandDataSource, _updateEpisodesService);

            _subscription = CreateSubscription(1, 555, "The Stuff", DateTime.Now.AddDays(-2).Date);
        }

        [Fact]
        public void Should_refresh_episodes_for_show()
        {
            var lastAirDate = DateTime.Now;
            _updateEpisodesService.UpdateEpisodesForSubscription(Arg.Any<Subscription>()).Returns(lastAirDate);
            _subscriptionQueryDataSource.GetSubscription(1).Returns(_subscription);
            var command = new RefreshEpisodesCommand { SubscriptionId = 1 };

            _handler.Handle(command);

            _episodeCommandDataSource.Received(1).DeleteAllFromSubscription(_subscription.Id);
            _updateEpisodesService.Received(1).UpdateEpisodesForSubscription(Arg.Is<Subscription>(s => s.Id == _subscription.Id && s.TvShowId == _subscription.TvShowId && s.TvShowName == _subscription.TvShowName && s.LastAirDate == DateTime.Now.Date));
            _subscriptionCommandDataSource.Received(1).SaveLastAirDate(_subscription.Id, lastAirDate.Date);
        }

        private Subscription CreateSubscription(int id, int tvShowId, string tvShowName, DateTime lastAirDate)
        {
            return new Subscription { Id = id, TvShowId = tvShowId, TvShowName = tvShowName, LastAirDate = lastAirDate };
        }
    }
}
