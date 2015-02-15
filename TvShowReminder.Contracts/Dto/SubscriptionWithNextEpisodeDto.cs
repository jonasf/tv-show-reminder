namespace TvShowReminder.Contracts.Dto
{
    public class SubscriptionWithNextEpisodeDto
    {
        public Subscription Subscription { get; set; }
        public Episode NextEpisode { get; set; }
    }
}