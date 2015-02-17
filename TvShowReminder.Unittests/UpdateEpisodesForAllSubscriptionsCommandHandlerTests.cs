using System;
using System.Collections.Generic;
using NSubstitute;
using TvShowReminder.Contracts.Command;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Command;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class UpdateEpisodesForAllSubscriptionsCommandHandlerTests
    {
        private readonly UpdateEpisodesForAllSubscriptionsCommandHandler _handler;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private readonly IUpdateEpisodesService _updateEpisodesService;

        private readonly Subscription _subscription1;
        private readonly Subscription _subscription2;

        public UpdateEpisodesForAllSubscriptionsCommandHandlerTests()
        {
            _subscriptionQueryDataSource = Substitute.For<ISubscriptionQueryDataSource>();
            _subscriptionCommandDataSource = Substitute.For<ISubscriptionCommandDataSource>();
            _updateEpisodesService = Substitute.For<IUpdateEpisodesService>();
            _handler = new UpdateEpisodesForAllSubscriptionsCommandHandler(_subscriptionQueryDataSource, _subscriptionCommandDataSource, _updateEpisodesService);

            _subscription1 = CreateSubscription(1, 555, "The Stuff", DateTime.Now.AddDays(-2).Date);
            _subscription2 = CreateSubscription(2, 666, "The Stuff 2", DateTime.Now.AddDays(-3).Date);
        }

        [Fact]
        public void Should_update_episodes_for_all_subscriptions()
        {
            var subscription1Airdate = DateTime.Now.AddDays(2);
            var subscription2Airdate = DateTime.Now.AddDays(5);
            _updateEpisodesService.UpdateEpisodesForSubscription(Arg.Is<Subscription>(s => s.Id == _subscription1.Id)).Returns(subscription1Airdate);
            _updateEpisodesService.UpdateEpisodesForSubscription(Arg.Is<Subscription>(s => s.Id == _subscription2.Id)).Returns(subscription2Airdate);
            _subscriptionQueryDataSource.GetAllSubscriptions().Returns(new List<Subscription> { _subscription1, _subscription2 });

            _handler.Handle(new UpdateEpisodesForAllSubscriptionsCommand());

            _updateEpisodesService.Received(1).UpdateEpisodesForSubscription(Arg.Is<Subscription>(s => s.Id == _subscription1.Id && s.TvShowId == _subscription1.TvShowId && s.TvShowName == _subscription1.TvShowName && s.LastAirDate == _subscription1.LastAirDate.Date));
            _updateEpisodesService.Received(1).UpdateEpisodesForSubscription(Arg.Is<Subscription>(s => s.Id == _subscription2.Id && s.TvShowId == _subscription2.TvShowId && s.TvShowName == _subscription2.TvShowName && s.LastAirDate == _subscription2.LastAirDate.Date));
            _subscriptionCommandDataSource.Received(1).SaveLastAirDate(_subscription1.Id, subscription1Airdate.Date);
            _subscriptionCommandDataSource.Received(1).SaveLastAirDate(_subscription2.Id, subscription2Airdate.Date);
        }

        private Subscription CreateSubscription(int id, int tvShowId, string tvShowName, DateTime lastAirDate)
        {
            return new Subscription { Id = id, TvShowId = tvShowId, TvShowName = tvShowName, LastAirDate = lastAirDate };
        }
    }
}
