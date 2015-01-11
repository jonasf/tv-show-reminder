using System;
using System.Linq;
using Xunit;

namespace TvShowReminder.TvRageApi.Unittests
{
    public class EpisodeListParserTests
    {
        [Fact]
        public void Should_return_5_episodes_and_3_specials()
        {
            var result = EpisodeListParser.Parse(GetXmlTestData());
            Assert.Equal(5, result.Episodes.Count());
            Assert.Equal(3, result.SpecialEpisodes.Count());
        }

        [Fact]
        public void Should_map_episode_properties_correctly()
        {
            var result = EpisodeListParser.Parse(GetXmlTestData());
            var item = result.Episodes.First();

            Assert.Equal(1, item.EpNum);
            Assert.Equal(1, item.SeasonNum);
            Assert.Equal("101", item.ProdNum);
            Assert.Equal(new DateTime(2005, 03, 26), item.AirDate);
            Assert.Equal("http://www.tvrage.com/DoctorWho_2005/episodes/52117", item.Link);
            Assert.Equal("Rose", item.Title);
        }

        [Fact]
        public void Should_map_special_episode_properties_correctly()
        {
            var result = EpisodeListParser.Parse(GetXmlTestData());
            var item = result.SpecialEpisodes.First();

            Assert.Equal(1, item.Season);
            Assert.Equal(new DateTime(2005, 11, 18), item.AirDate);
            Assert.Equal("http://www.tvrage.com/DoctorWho_2005/episodes/580932", item.Link);
            Assert.Equal("Born Again : Children in Need", item.Title);
        }

        private string GetXmlTestData()
        {
            return @"<Show>
                        <name>Doctor Who</name>
                        <totalseasons>8</totalseasons>
                        <Episodelist>
                        <Season no=""1"">
                        <episode>
                        <epnum>1</epnum>
                        <seasonnum>01</seasonnum>
                        <prodnum>101</prodnum>
                        <airdate>2005-03-26</airdate>
                        <link>http://www.tvrage.com/DoctorWho_2005/episodes/52117</link>
                        <title>Rose</title>
                        </episode>
                        <episode>
                        <epnum>2</epnum>
                        <seasonnum>02</seasonnum>
                        <prodnum>102</prodnum>
                        <airdate>2005-04-02</airdate>
                        <link>
                        http://www.tvrage.com/DoctorWho_2005/episodes/52118
                        </link>
                        <title>The End of the World</title>
                        </episode>
                        </Season>
                        <Season no=""2"">
                        <episode>
                        <epnum>14</epnum>
                        <seasonnum>01</seasonnum>
                        <prodnum>14</prodnum>
                        <airdate>2006-04-15</airdate>
                        <link>
                        http://www.tvrage.com/DoctorWho_2005/episodes/52131
                        </link>
                        <title>New Earth</title>
                        </episode>
                        <episode>
                        <epnum>15</epnum>
                        <seasonnum>02</seasonnum>
                        <prodnum>15</prodnum>
                        <airdate>2006-04-22</airdate>
                        <link>
                        http://www.tvrage.com/DoctorWho_2005/episodes/52132
                        </link>
                        <title>Tooth and Claw</title>
                        </episode>
                        <episode>
                        <epnum>16</epnum>
                        <seasonnum>03</seasonnum>
                        <prodnum>16</prodnum>
                        <airdate>2006-04-29</airdate>
                        <link>
                        http://www.tvrage.com/DoctorWho_2005/episodes/52133
                        </link>
                        <title>School Reunion</title>
                        </episode>
                        </Season>
                        <Special>
                        <episode>
                        <season>1</season>
                        <airdate>2005-11-18</airdate>
                        <link>http://www.tvrage.com/DoctorWho_2005/episodes/580932</link>
                        <title>Born Again : Children in Need</title>
                        </episode>
                        <episode>
                        <season>1</season>
                        <airdate>2005-12-25</airdate>
                        <link>
                        http://www.tvrage.com/DoctorWho_2005/episodes/52130
                        </link>
                        <title>The Christmas Invasion</title>
                        </episode>
                        <episode>
                        <season>2</season>
                        <airdate>2005-12-25</airdate>
                        <link>
                        http://www.tvrage.com/DoctorWho_2005/episodes/1065021570
                        </link>
                        <title>Attack of the Graske</title>
                        </episode>
                        </Special>
                        </Episodelist>
                        </Show>";
        }
    }
}
