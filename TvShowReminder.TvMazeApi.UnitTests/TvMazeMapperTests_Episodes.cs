using System;
using System.Collections.Generic;
using System.Linq;
using TvShowReminder.TvMazeApi.Domain;
using TvShowReminder.TvMazeApi.Utilities;
using Xunit;

namespace TvShowReminder.TvMazeApi.UnitTests
{
    public class TvMazeMapperTests_Episodes
    {
        private readonly IEnumerable<TvMazeEpisode> _result;

        public TvMazeMapperTests_Episodes()
        {
            _result = TvMazeMapper.MapEpisodes(CreateTestJson());
        }

        [Fact]
        public void Should_map_episode_json_to_dto()
        {
            Assert.NotNull(_result);
            Assert.NotEmpty(_result);
            Assert.Equal(_result.Count(), 3);
        }

        [Fact]
        public void Should_map_common_properties_for_episode()
        {
            var episode = _result.First();

            Assert.Equal("Face the Raven", episode.Name);
            Assert.Equal(9, episode.Season);
            Assert.Equal(new DateTime(2015, 11, 21), episode.AirDate);
        }

        [Fact]
        public void Should_map_episode_number_for_normal_episode()
        {
            var episode = _result.First();

            Assert.Equal(10, episode.Number);
        }

        [Fact]
        public void Should_map_episode_number_for_special_episode()
        {
            var episode = _result.Last();

            Assert.Equal(0, episode.Number);
        }

        private string CreateTestJson()
        {
            return @"[ { ""_links"" : { ""self"" : { ""href"" : ""http://api.tvmaze.com/episodes/228570"" } },
                        ""airdate"" : ""2015-11-21"",
                        ""airstamp"" : ""2015-11-21T19:30:00+00:00"",
                        ""airtime"" : ""19:30"",
                        ""id"" : 228570,
                        ""image"" : null,
                        ""name"" : ""Face the Raven"",
                        ""number"" : 10,
                        ""runtime"" : 60,
                        ""season"" : 9,
                        ""summary"" : ""<p>Have you ever found yourself in a street you've never seen before? The next day, could you not find that street again? You weren't dreaming. Your memory isn't playing tricks. Like many lost souls throughout the ages, you have stumbled on an extraordinary secret - be grateful you survived it. The Doctor and Clara, with their old friend Rigsy, find themselves in a secret alien world, folded away among the streets of London. Not all of them will get out alive. One of the three intruders must face the raven...</p>"",
                        ""url"" : ""http://www.tvmaze.com/episodes/228570/doctor-who-9x10-face-the-raven""
                        },
                        { ""_links"" : { ""self"" : { ""href"" : ""http://api.tvmaze.com/episodes/228571"" } },
                        ""airdate"" : ""2015-11-28"",
                        ""airstamp"" : ""2015-11-28T19:30:00+00:00"",
                        ""airtime"" : ""19:30"",
                        ""id"" : 228571,
                        ""image"" : null,
                        ""name"" : ""Heaven Sent (1)"",
                        ""number"" : 11,
                        ""runtime"" : 60,
                        ""season"" : 9,
                        ""summary"" : ""<p>In a world unlike any other he has seen, the Doctor faces the greatest challenge of his many lives. And he must face it alone.</p>"",
                        ""url"" : ""http://www.tvmaze.com/episodes/228571/doctor-who-9x11-heaven-sent-1""
                        },
                        { ""_links"" : { ""self"" : { ""href"" : ""http://api.tvmaze.com/episodes/13971"" } },
                        ""airdate"" : ""2009-11-15"",
                        ""airstamp"" : ""2009-11-15T19:35:00+00:00"",
                        ""airtime"" : ""19:35"",
                        ""id"" : 13971,
                        ""image"" : null,
                        ""name"" : ""The Waters of Mars"",
                        ""number"" : null,
                        ""runtime"" : 45,
                        ""season"" : 4,
                        ""summary"" : ""<p>Mars, 2059. Bowie Base One. Last recorded message: \""Don't drink the water. Don't even touch it. Not one drop.\""</p><p>Adelaide and the Doctor face terror on the Red Planet in one of the scariest adventures yet.</p>"",
                        ""url"" : ""http://www.tvmaze.com/episodes/13971/doctor-who-s04-special-the-waters-of-mars""
                        }
                    ]";
        }
    }
}
