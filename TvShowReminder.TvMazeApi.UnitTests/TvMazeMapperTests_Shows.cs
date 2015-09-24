using System;
using System.Collections.Generic;
using System.Linq;
using TvShowReminder.TvMazeApi.Domain;
using TvShowReminder.TvMazeApi.Utilities;
using Xunit;

namespace TvShowReminder.TvMazeApi.UnitTests
{
    public class TvMazeMapperTests_Shows
    {
        private readonly IEnumerable<TvMazeShow> _result;

        public TvMazeMapperTests_Shows()
        {
            _result = TvMazeMapper.MapShows(CreateTestJson());
        }

        [Fact]
        public void Should_map_show_json_to_dto()
        {
            Assert.NotNull(_result);
            Assert.NotEmpty(_result);
            Assert.Equal(_result.Count(), 2);
        }

        [Fact]
        public void Should_map_properties()
        {
            var show = _result.First();

            Assert.Equal(210, show.Id);
            Assert.Equal("Doctor Who", show.Name);
            Assert.Equal(new DateTime(2005, 3, 26), show.Premiered);
            Assert.Equal("http://www.tvmaze.com/shows/210/doctor-who", show.Url);
            Assert.Equal("http://tvmazecdn.com/uploads/images/medium_portrait/1/3179.jpg", show.Image.Medium);
            Assert.Equal("http://tvmazecdn.com/uploads/images/original_untouched/1/3179.jpg", show.Image.Original);
        }

        private string CreateTestJson()
        {
            return @"[ { ""score"" : 5.389303,
                        ""show"" : { ""_links"" : { ""nextepisode"" : { ""href"" : ""http://api.tvmaze.com/episodes/185063"" },
                                ""previousepisode"" : { ""href"" : ""http://api.tvmaze.com/episodes/185062"" },
                                ""self"" : { ""href"" : ""http://api.tvmaze.com/shows/210"" }
                                },
                            ""externals"" : { ""thetvdb"" : 78804,
                                ""tvrage"" : 3332
                                },
                            ""genres"" : [ ""Action"",
                                ""Adventure"",
                                ""Science-Fiction""
                                ],
                            ""id"" : 210,
                            ""image"" : { ""medium"" : ""http://tvmazecdn.com/uploads/images/medium_portrait/1/3179.jpg"",
                                ""original"" : ""http://tvmazecdn.com/uploads/images/original_untouched/1/3179.jpg""
                                },
                            ""language"" : ""English"",
                            ""name"" : ""Doctor Who"",
                            ""network"" : { ""country"" : { ""code"" : ""GB"",
                                    ""name"" : ""United Kingdom"",
                                    ""timezone"" : ""Europe/London""
                                    },
                                ""id"" : 12,
                                ""name"" : ""BBC one""
                                },
                            ""premiered"" : ""2005-03-26"",
                            ""rating"" : { ""average"" : 9.0999999999999996 },
                            ""runtime"" : 60,
                            ""schedule"" : { ""days"" : [ ""Saturday"" ],
                                ""time"" : ""19:30""
                                },
                            ""status"" : ""Running"",
                            ""summary"" : ""<p>Adventures across time and space with the time-travelling hero.</p>"",
                            ""type"" : ""Scripted"",
                            ""updated"" : 1443036479,
                            ""url"" : ""http://www.tvmaze.com/shows/210/doctor-who"",
                            ""webChannel"" : null,
                            ""weight"" : 3
                            }
                        },
                        { ""score"" : 5.0213675000000002,
                        ""show"" : { ""_links"" : { ""nextepisode"" : { ""href"" : ""http://api.tvmaze.com/episodes/236867"" },
                                ""previousepisode"" : { ""href"" : ""http://api.tvmaze.com/episodes/215527"" },
                                ""self"" : { ""href"" : ""http://api.tvmaze.com/shows/3142"" }
                                },
                            ""externals"" : { ""thetvdb"" : 299845,
                                ""tvrage"" : 50437
                                },
                            ""genres"" : [ ""Drama"",
                                ""Medical""
                                ],
                            ""id"" : 3142,
                            ""image"" : { ""medium"" : ""http://tvmazecdn.com/uploads/images/medium_portrait/17/43166.jpg"",
                                ""original"" : ""http://tvmazecdn.com/uploads/images/original_untouched/17/43166.jpg""
                                },
                            ""language"" : null,
                            ""name"" : ""Doctor Foster"",
                            ""network"" : { ""country"" : { ""code"" : ""GB"",
                                    ""name"" : ""United Kingdom"",
                                    ""timezone"" : ""Europe/London""
                                    },
                                ""id"" : 12,
                                ""name"" : ""BBC one""
                                },
                            ""premiered"" : ""2015-09-09"",
                            ""rating"" : { ""average"" : 10 },
                            ""runtime"" : 60,
                            ""schedule"" : { ""days"" : [ ""Wednesday"" ],
                                ""time"" : ""21:00""
                                },
                            ""status"" : ""Running"",
                            ""summary"" : ""<p>A trusted GP sees her charmed life explode when she suspects her husband of an affair. As she uncovers secrets that shock her to the core, how will Dr Gemma Foster react?</p>"",
                            ""type"" : ""Scripted"",
                            ""updated"" : 1443051838,
                            ""url"" : ""http://www.tvmaze.com/shows/3142/doctor-foster"",
                            ""webChannel"" : null,
                            ""weight"" : 1
                            }
                        }
                    ]";
        }
    }
}
