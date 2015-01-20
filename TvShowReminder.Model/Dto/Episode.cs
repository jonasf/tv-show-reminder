using System;

namespace TvShowReminder.Model.Dto
{
    public class Episode
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public DateTime AirDate { get; set; }
    }
}
