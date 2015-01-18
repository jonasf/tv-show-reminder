using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TvShowReminder.TvRageApi.Domain;

namespace TvShowReminder.TvRageApi.Utilities
{
    public class EpisodeListParser
    {
        public static TvRageEpisodeList Parse(string rawXml)
        {
            var xDoc = XDocument.Parse(rawXml);

            if (xDoc.Root == null)
                return new TvRageEpisodeList { Episodes = new List<TvRageEpisode>(), SpecialEpisodes = new List<TvRageSpecialEpisode>() };

            var episodes = ParseEpisodes(rawXml);
            var specialEpisodes = ParseSpecialEpisodes(rawXml);

            return new TvRageEpisodeList
            {
                Episodes = episodes,
                SpecialEpisodes = specialEpisodes
            };
        }

        private static IEnumerable<TvRageEpisode> ParseEpisodes(string rawXml)
        {
            var xDoc = XDocument.Parse(rawXml);

            var seasons = xDoc.Root.Descendants("Season");
            return seasons
                    .Elements("episode")
                    .Select(x => new TvRageEpisode
                    {
                        Season = (int)x.Parent.Attribute("no"),
                        EpNum = (int)x.Element("epnum"),
                        SeasonNum = (int)x.Element("seasonnum"),
                        ProdNum = (string)x.Element("prodnum"),
                        AirDate = ParseDate((string)x.Element("airdate")),
                        Link = (string)x.Element("link"),
                        Title = (string)x.Element("title")
                    });
        }

        private static IEnumerable<TvRageSpecialEpisode> ParseSpecialEpisodes(string rawXml)
        {
            var xDoc = XDocument.Parse(rawXml);
            var specials = xDoc.Root.Descendants("Special");
            return specials
                    .Elements("episode")
                    .Select(x => new TvRageSpecialEpisode
                    {
                        Season = (int)x.Element("season"),
                        AirDate = ParseDate((string)x.Element("airdate")),
                        Link = (string)x.Element("link"),
                        Title = (string)x.Element("title")
                    });
        }

        private static DateTime ParseDate(string dateString)
        {
            try
            {
                return DateTime.Parse(dateString);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }
}