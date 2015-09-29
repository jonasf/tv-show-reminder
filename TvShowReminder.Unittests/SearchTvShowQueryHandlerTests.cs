using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TvShowReminder.Contracts.Query;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Query;
using TvShowReminder.TvMazeApi;
using TvShowReminder.TvMazeApi.Domain;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class SearchTvShowQueryHandlerTests
    {
        private readonly SearchTvShowQueryHandler _handler;
        private readonly ITvMazeService _tvMazeService;
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;

        private readonly TvMazeShow _show1;
        private readonly TvMazeShow _show2;

        public SearchTvShowQueryHandlerTests()
        {
            _show1 = CreateShow1();
            _show2 = CreateShow2();

            _tvMazeService = Substitute.For<ITvMazeService>();
            _subscriptionQueryDataSource = Substitute.For<ISubscriptionQueryDataSource>();
            _handler = new SearchTvShowQueryHandler(_tvMazeService, _subscriptionQueryDataSource);
        }

        [Fact]
        public void Should_return_search_result()
        {
            const string query = "The awesome show";
            _tvMazeService.Search(query).Returns(CreateApiResponse());

            var result = _handler.Handle(new SearchTvShowQuery { Query = query });

            Assert.NotNull(result.TvShows);
            Assert.True(result.TvShows.Count() == 2);
        }

        [Fact]
        public void Should_map_properties()
        {
            const string query = "The awesome show";
            _tvMazeService.Search(query).Returns(CreateApiResponse());

            var result = _handler.Handle(new SearchTvShowQuery { Query = query });

            var show1 = result.TvShows.First();
            Assert.Equal(_show1.Id, show1.Id);
            Assert.Equal(_show1.Name, show1.Name);
            Assert.Equal(_show1.Url, show1.Link);
            Assert.Equal(_show1.Premiered.Year, show1.StartedYear);
            Assert.Equal(_show1.Image.Medium, show1.ImageUrl);
            var show2 = result.TvShows.ElementAt(1);
            Assert.Equal(_show2.Id, show2.Id);
            Assert.Equal(_show2.Name, show2.Name);
            Assert.Equal(_show2.Url, show2.Link);
            Assert.Equal(_show2.Premiered.Year, show2.StartedYear);
            Assert.Equal(_show2.Image.Medium, show2.ImageUrl);
        }

        [Fact]
        public void Should_flag_already_subscribed_show()
        {
            const string query = "The awesome show";
            _tvMazeService.Search(query).Returns(CreateApiResponse());
            _subscriptionQueryDataSource.GetAllSubscriptionIds().Returns(new List<int> { 555, 888, 999 });

            var result = _handler.Handle(new SearchTvShowQuery { Query = query });
            var show1 = result.TvShows.First();
            Assert.Equal(true, show1.IsSubscribed);
        }

        [Fact]
        public void Should_handle_empty_image_data()
        {
            const string query = "The awesome show";
            _tvMazeService.Search(query).Returns(new List<TvMazeShow> { CreateShowWithoutImage() });

            var result = _handler.Handle(new SearchTvShowQuery { Query = query });

            var show = result.TvShows.First();
            Assert.NotNull(show.ImageUrl);
            Assert.True(show.ImageUrl == string.Empty);
        }

        private IEnumerable<TvMazeShow> CreateApiResponse()
        {
            return new List<TvMazeShow>
            {
                _show1,
                _show2
            };
        }

        private TvMazeShow CreateShow1()
        {
            return new TvMazeShow
            {
                Id = 555,
                Name = "Almost awesome",
                Premiered = new DateTime(1992,1,1),
                Url = "url1",
                Image = new TvMazeShowImage
                {
                    Medium = "medium1",
                    Original = "original1"
                }
            };
        }

        private TvMazeShow CreateShow2()
        {
            return new TvMazeShow
            {
                Id = 666,
                Name = "Not nearly awesome",
                Premiered = new DateTime(2001, 2, 2),
                Url = "url2",
                Image = new TvMazeShowImage
                {
                    Medium = "medium2",
                    Original = "original2"
                }
            };
        }

        private TvMazeShow CreateShowWithoutImage()
        {
            return new TvMazeShow
            {
                Id = 666,
                Name = "Not nearly awesome",
                Premiered = new DateTime(2001, 2, 2),
                Url = "url2",
                Image = null
            };
        }
    }
}
