using System;

namespace TvShowReminder.Contracts.Dto
{
    public class Subscription
    {
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public string TvShowName { get; set; }
        public DateTime LastAirDate { get; set; }
    }
}
