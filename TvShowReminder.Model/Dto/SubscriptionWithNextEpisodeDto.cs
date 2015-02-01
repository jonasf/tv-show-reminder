namespace TvShowReminder.Model.Dto
{
    public class SubscriptionWithNextEpisodeDto
    {
        public Subscription Subscription { get; set; }
        public Episode NextEpisode { get; set; }
    }
}