using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.DataSource;
using TvShowReminder.Service;
using TvShowReminder.TvRageApi;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class SubscriptionQueryServiceTests
    {
        private readonly SubscriptionQueryService _subscriptionQueryService;
        private readonly ITvRageService _tvRageService;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly IEpisodesQueryDataSource _episodesQueryDataSource;


        public SubscriptionQueryServiceTests()
        {
            _tvRageService = Substitute.For<ITvRageService>();
            _subscriptionQueryDataSource = Substitute.For<ISubscriptionQueryDataSource>();
            _episodesQueryDataSource = Substitute.For<IEpisodesQueryDataSource>();
            _subscriptionQueryService = new SubscriptionQueryService(_tvRageService, _subscriptionQueryDataSource, _episodesQueryDataSource);
        }

        [Fact]
        public void Should_get_next_show_for_each_subscription()
        {
            _subscriptionQueryDataSource.GetAllSubscriptions().Returns(new List<Subscription> {CreateSubscription()});
            _episodesQueryDataSource.GetNextEpisode(1)
                .Returns(new Episode
                {
                    AirDate = DateTime.Now.AddDays(1),
                    Title = "Hello",
                    SeasonNumber = 1,
                    EpisodeNumber = 2
                });

            var result = _subscriptionQueryService.GetAllWithNextEpisode();

            _episodesQueryDataSource.Received(1).GetNextEpisode(1);

            var subscription = result.Subscriptions.First();
            Assert.Equal(subscription.Subscription.TvShowId, 555);
            Assert.Equal(subscription.NextEpisode.Title, "Hello");
        }

        private Subscription CreateSubscription()
        {
            return new Subscription
            {
                Id = 1,
                TvShowId = 555,
                TvShowName = "The stuff",
                LastAirDate = DateTime.Now.AddDays(5)
            };
        }
    }
}
