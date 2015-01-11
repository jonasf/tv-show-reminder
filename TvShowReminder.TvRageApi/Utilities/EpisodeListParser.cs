using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TvShowReminder.TvRageApi.Domain;

namespace TvShowReminder.TvRageApi.Utilities
{
    public class EpisodeListParser
    {
        public static EpisodeList Parse(string rawXml)
        {
            var xDoc = XDocument.Parse(rawXml);

            if (xDoc.Root == null)
                return new EpisodeList { Episodes = new List<Episode>(), SpecialEpisodes = new List<SpecialEpisode>() };

            var episodes = ParseEpisodes(rawXml);
            var specialEpisodes = ParseSpecialEpisodes(rawXml);

            return new EpisodeList
            {
                Episodes = episodes,
                SpecialEpisodes = specialEpisodes
            };
        }

        private static IEnumerable<Episode> ParseEpisodes(string rawXml)
        {
            var xDoc = XDocument.Parse(rawXml);

            var seasons = xDoc.Root.Descendants("Season");
            return seasons
                    .Elements("episode")
                    .Select(x => new Episode
                    {
                        EpNum = (int)x.Element("epnum"),
                        SeasonNum = (int)x.Element("seasonnum"),
                        ProdNum = (string)x.Element("prodnum"),
                        AirDate = DateTime.Parse((string)x.Element("airdate")),
                        Link = (string)x.Element("link"),
                        Title = (string)x.Element("title")
                    });
        }

        private static IEnumerable<SpecialEpisode> ParseSpecialEpisodes(string rawXml)
        {
            var xDoc = XDocument.Parse(rawXml);
            var specials = xDoc.Root.Descendants("Special");
            return specials
                    .Elements("episode")
                    .Select(x => new SpecialEpisode
                    {
                        Season = (int)x.Element("season"),
                        AirDate = DateTime.Parse((string)x.Element("airdate")),
                        Link = (string)x.Element("link"),
                        Title = (string)x.Element("title")
                    });
        }
    }
}