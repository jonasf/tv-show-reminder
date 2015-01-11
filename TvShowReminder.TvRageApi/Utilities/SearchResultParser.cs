using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TvShowReminder.TvRageApi.Domain;

namespace TvShowReminder.TvRageApi.Utilities
{
    public static class SearchResultParser
    {
        public static IEnumerable<Show> Parse(string rawXml)
        {
            var xDoc = XDocument.Parse(rawXml);

            if (xDoc.Root == null)
                return new List<Show>();

            var shows = ParseShows(xDoc).ToList();
            return shows;
        }

        private static IEnumerable<Show> ParseShows(XDocument xDoc)
        {
            return xDoc.Root
                .Elements("show")
                .Select(x => new Show
                {
                    ShowId = (int)x.Element("showid"),
                    Name = (string)x.Element("name"),
                    Link = (string)x.Element("link"),
                    Country = (string)x.Element("country"),
                    Started = (int)x.Element("started"),
                    Seasons = (int)x.Element("seasons"),
                    Ended = (int)x.Element("ended"),
                    Status = (string)x.Element("status"),
                    Classification = (string)x.Element("classification"),
                    Genres = ParseGenres(x.Element("genres")).ToList()
                });
        }

        private static IEnumerable<string> ParseGenres(XElement genres)
        {
            return genres.Descendants("genre").Select(x => x.Value);
        }
    }
}
