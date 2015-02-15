using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Query;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class EpisodesToDateQueryHandlerTests
    {
        private readonly EpisodesToDateQueryHandler _handler;
        private readonly IEpisodesQueryDataSource _episodesQueryDataSource;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        private readonly EpisodesToDateQuery _query;
        private readonly DateTime _episodesToDate = DateTime.Now;

        public EpisodesToDateQueryHandlerTests()
        {
            _query = new EpisodesToDateQuery { ToDate = _episodesToDate };

            _episodesQueryDataSource = Substitute.For<IEpisodesQueryDataSource>();
            _subscriptionQueryDataSource = Substitute.For<ISubscriptionQueryDataSource>();
            _handler = new EpisodesToDateQueryHandler(_episodesQueryDataSource, _subscriptionQueryDataSource);   
        }

        [Fact]
        public void Should_return_list_of_episodes()
        {
            _episodesQueryDataSource.GetToDate(_episodesToDate).Returns(CreateEpisodesList());
            _subscriptionQueryDataSource.GetAllSubscriptions().Returns(CreateSubscriptionList());

            var result = _handler.Handle(_query);

            Assert.NotNull(result);
            Assert.NotNull(result.Episodes);
            Assert.Equal(4, result.Episodes.Count());
        }

        [Fact]
        public void Should_have_subscription_info_on_episodes()
        {
            _episodesQueryDataSource.GetToDate(_episodesToDate).Returns(CreateEpisodesList());
            _subscriptionQueryDataSource.GetAllSubscriptions().Returns(CreateSubscriptionList());

            var result = _handler.Handle(_query);

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
