using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TvShowReminder.DataSource;
using TvShowReminder.Model.Dto;
using TvShowReminder.Model.Query;
using TvShowReminder.Service;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class EpisodesQueryServiceTests
    {
        private EpisodesQueryService _episodesQueryService;

        private IEpisodesQueryDataSource _episodesQueryDataSource;
        private ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        private readonly DateTime _episodesToDate = DateTime.Now;

        public EpisodesQueryServiceTests()
        {
            _episodesQueryDataSource = Substitute.For<IEpisodesQueryDataSource>();
            _subscriptionQueryDataSource = Substitute.For<ISubscriptionQueryDataSource>();

            _episodesQueryService = new EpisodesQueryService(_episodesQueryDataSource, _subscriptionQueryDataSource);
        }

        [Fact]
        public void Should_return_episodes()
        {
            _episodesQueryDataSource.GetToDate(_episodesToDate).Returns(CreateEpisodesList());
            _subscriptionQueryDataSource.GetAllSubscriptions().Returns(CreateSubscriptionList());
            var query = new EpisodesToDateQuery { ToDate = _episodesToDate };

            var result = _episodesQueryService.GetEpisodesUpToDate(query);

            Assert.NotNull(result);
            Assert.NotNull(result.Episodes);
            Assert.Equal(4, result.Episodes.Count());
        }

        [Fact]
        public void Should_have_subscription_info_on_episodes()
        {
            _episodesQueryDataSource.GetToDate(_episodesToDate).Returns(CreateEpisodesList());
            _subscriptionQueryDataSource.GetAllSubscriptions().Returns(CreateSubscriptionList());
            var query = new EpisodesToDateQuery { ToDate = _episodesToDate };

            var result = _episodesQueryService.GetEpisodesUpToDate(query);

            var show1 = result.Episodes.First();
            Assert.NotNull(show1.Subscription);
            Assert.IsType<Subscription>(show1.Subscription);
            Assert.Equal(1, show1.Subscription.Id);

            var show2 = result.Episodes.ElementAt(1);
            Assert.NotNull(show2.Subscription);
            Assert.IsType<Subscription>(show2.Subscription);
            Assert.Equal(2, show2.Subscription.Id);
        }

        private IEnumerable<Episode> CreateEpisodesList()
        {
            return new List<Episode>
            {
                new Episode { Id = 123, SubscriptionId = 1, SeasonNumber = 1, EpisodeNumber = 3 },
                new Episode { Id = 234, SubscriptionId = 2, SeasonNumber = 5, EpisodeNumber = 8 },
                new Episode { Id = 456, SubscriptionId = 1, SeasonNumber = 1, EpisodeNumber = 4 },
                new Episode { Id = 678, SubscriptionId = 2, SeasonNumber = 5, EpisodeNumber = 9 }
            };
        }

        private IEnumerable<Subscription> CreateSubscriptionList()
        {
            return new List<Subscription>
            {
                new Subscription { Id = 1, TvShowId = 1, TvShowName = "The awesome show" },
                new Subscription { Id = 2, TvShowId = 2, TvShowName = "The horrible show" }
            };
        } 
    }
}
