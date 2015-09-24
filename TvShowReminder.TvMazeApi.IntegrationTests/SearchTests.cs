using System.Linq;
using Xunit;

namespace TvShowReminder.TvMazeApi.IntegrationTests
{
    public class SearchTests
    {
        private readonly TvMazeService _tvMazeService;

        public SearchTests()
        {
            _tvMazeService = new TvMazeService();
        }

        [Fact(Skip = "Only for integration tests")]
        public void Should_return_list_of_shows()
        {
            var result = _tvMazeService.Search("Doctor Who");
            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
