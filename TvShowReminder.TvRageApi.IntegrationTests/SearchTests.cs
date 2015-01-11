using System.Linq;
using Xunit;

namespace TvShowReminder.TvRageApi.IntegrationTests
{
    public class SearchTests
    {
        private readonly TvRageService _tvRageService;

        public SearchTests()
        {
            _tvRageService = new TvRageService();    
        }

        [Fact(Skip = "Only for integration tests")]
        public void Should_return_list_of_shows()
        {
            var result = _tvRageService.Search("SuperNatural");
            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
