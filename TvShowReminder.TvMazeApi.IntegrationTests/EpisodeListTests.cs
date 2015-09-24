using System.Linq;
using Xunit;

namespace TvShowReminder.TvMazeApi.IntegrationTests
{
    public class EpisodeListTests
    {
        private readonly TvMazeService _tvMazeService;

        public EpisodeListTests()
        {
            _tvMazeService = new TvMazeService();
        }

        [Fact(Skip = "Only for integration tests")]
        public void Should_return_list_of_episodes()
        {
            var result = _tvMazeService.GetEpisodes(210);
            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
