using System.Linq;
using TvShowReminder.TvRageApi.Utilities;
using Xunit;

namespace TvShowReminder.TvRageApi.Unittests
{
    public class SearchResultParserTests
    {
        [Fact]
        public void Should_return_2_items()
        {
            var result = SearchResultParser.Parse(GetXmlTestData());
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_map_properties_correctly()
        {
            var result = SearchResultParser.Parse(GetXmlTestData());
            var item = result.First();

            Assert.Equal(2930, item.ShowId);
            Assert.Equal("Buffy the Vampire Slayer", item.Name);
            Assert.Equal("http://www.tvrage.com/Buffy_The_Vampire_Slayer", item.Link);
            Assert.Equal("US", item.Country);
            Assert.Equal(1997, item.Started);
            Assert.Equal(2003, item.Ended);
            Assert.Equal(7, item.Seasons);
            Assert.Equal("Ended", item.Status);
            Assert.Equal("Scripted", item.Classification);
            Assert.Equal(7, item.Genres.Count());

            Assert.Contains("Action", item.Genres);
            Assert.Contains("Adventure", item.Genres);
            Assert.Contains("Comedy", item.Genres);
            Assert.Contains("Drama", item.Genres);
            Assert.Contains("Horror/Supernatural", item.Genres);
            Assert.Contains("Mystery", item.Genres);
            Assert.Contains("Sci-Fi", item.Genres);
        }

        private string GetXmlTestData()
        {
            return @"<Results>
                        <show>
                        <showid>2930</showid>
                        <name>Buffy the Vampire Slayer</name>
                        <link>http://www.tvrage.com/Buffy_The_Vampire_Slayer</link>
                        <country>US</country>
                        <started>1997</started>
                        <ended>2003</ended>
                        <seasons>7</seasons>
                        <status>Ended</status>
                        <classification>Scripted</classification>
                        <genres>
                        <genre>Action</genre>
                        <genre>Adventure</genre>
                        <genre>Comedy</genre>
                        <genre>Drama</genre>
                        <genre>Horror/Supernatural</genre>
                        <genre>Mystery</genre>
                        <genre>Sci-Fi</genre>
                        </genres>
                        </show>
                        <show>
                        <showid>31192</showid>
                        <name>
                        Buffy the Vampire Slayer - Season Eight: Motion comics
                        </name>
                        <link>
                        http://www.tvrage.com/buffy-the-vampire-slayer-season-eight-mo
                        </link>
                        <country>US</country>
                        <started>2010</started>
                        <ended>2010</ended>
                        <seasons>1</seasons>
                        <status>Canceled/Ended</status>
                        <classification>Animation</classification>
                        <genres>
                        <genre>Animation General</genre>
                        <genre>Action</genre>
                        <genre>Adventure</genre>
                        <genre>Comedy</genre>
                        <genre>Drama</genre>
                        <genre>Horror/Supernatural</genre>
                        <genre>Sci-Fi</genre>
                        </genres>
                        </show>
                        </Results>";
        }
    }
}
