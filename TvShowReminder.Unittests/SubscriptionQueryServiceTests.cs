﻿using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TvShowReminder.DataSource;
using TvShowReminder.Model.Query;
using TvShowReminder.Service;
using TvShowReminder.TvRageApi;
using TvShowReminder.TvRageApi.Domain;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class SubscriptionQueryServiceTests
    {
        private readonly SubscriptionQueryService _subscriptionQueryService;
        private readonly ITvRageService _tvRageService;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        private readonly Show _show1;
        private readonly Show _show2;

        public SubscriptionQueryServiceTests()
        {
            _tvRageService = Substitute.For<ITvRageService>();
            _subscriptionQueryDataSource = Substitute.For<ISubscriptionQueryDataSource>();
            _subscriptionQueryService = new SubscriptionQueryService(_tvRageService, _subscriptionQueryDataSource);

            _show1 = CreateShow1();
            _show2 = CreateShow2();
        }

        [Fact]
        public void Should_return_search_result()
        {
            const string query = "The awesome show";
            _tvRageService.Search(query).Returns(CreateApiResponse());

            var result = _subscriptionQueryService.Search(new SearchTvShowQuery { Query = query});

            Assert.NotNull(result.TvShows);
            Assert.True(result.TvShows.Count() == 2);
        }

        [Fact]
        public void Should_map_properties()
        {
            const string query = "The awesome show";
            _tvRageService.Search(query).Returns(CreateApiResponse());

            var result = _subscriptionQueryService.Search(new SearchTvShowQuery { Query = query });

            var show1 = result.TvShows.First();
            Assert.Equal(_show1.ShowId, show1.Id);
            Assert.Equal(_show1.Name, show1.Name);
            Assert.Equal(_show1.Link, show1.Link);
            Assert.Equal(_show1.Started, show1.StartedYear);
            Assert.Equal(_show1.Ended, show1.EndedYear);
            var show2 = result.TvShows.ElementAt(1);
            Assert.Equal(_show2.ShowId, show2.Id);
            Assert.Equal(_show2.Name, show2.Name);
            Assert.Equal(_show2.Link, show2.Link);
            Assert.Equal(_show2.Started, show2.StartedYear);
            Assert.Equal(_show2.Ended, show2.EndedYear);
        }

        [Fact]
        public void Should_flag_already_subscribed_show()
        {
            const string query = "The awesome show";
            _tvRageService.Search(query).Returns(CreateApiResponse());
            _subscriptionQueryDataSource.GetAllSubscriptionIds().Returns(new List<int> {555, 888, 999});

            var result = _subscriptionQueryService.Search(new SearchTvShowQuery { Query = query });
            var show1 = result.TvShows.First();
            Assert.Equal(true, show1.IsSubscribed);
        }

        private IEnumerable<Show> CreateApiResponse()
        {
            return new List<Show>
            {
                _show1,
                _show2
            };
        }

        private Show CreateShow1()
        {
            return new Show
            {
                Name = "Almost awesome",
                Country = "US",
                Seasons = 7,
                ShowId = 555,
                Link = "http://blabla.se",
                Classification = "Scripted",
                Genres = new List<string> {"Action", "Drama"},
                Status = "Running",
                Started = 1992,
                Ended = 2002
            };
        }

        private Show CreateShow2()
        {
            return new Show
            {
                Name = "Not nearly awesome",
                Country = "US",
                Seasons = 1,
                ShowId = 666,
                Link = "http://argh.se",
                Classification = "Scripted",
                Genres = new List<string> {"Sci-fi", "Fantasy"},
                Status = "Running",
                Started = 2001,
                Ended = 2002
            };
        }
    }
}
