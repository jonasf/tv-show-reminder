using System.Linq;
using Xunit;

namespace TvShowReminder.TvRageApi.IntegrationTests
{
    public class EpisodeListTests
    {
        private readonly TvRageService _tvRageService;

        public EpisodeListTests()
        {
            _tvRageService = new TvRageService();    
        }

        [Fact(Skip = "Only for integration tests")]
        public void Should_return_list_of_episodes()
        {
            var result = _tvRageService.GetEpisodes(3332);
            Assert.NotNull(result);
            Assert.True(result.Episodes.Any());
            Assert.True(result.SpecialEpisodes.Any());
        }
    }
}
