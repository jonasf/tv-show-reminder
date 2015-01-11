using System.Collections.Generic;

namespace TvShowReminder.TvRageApi.Domain
{
    public class Show
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Country { get; set; }
        public int Started { get; set; }
        public int Ended { get; set; }
        public int Seasons { get; set; }
        public string Status { get; set; }
        public string Classification { get; set; }
        public IEnumerable<string> Genres { get; set; }
    }
}
