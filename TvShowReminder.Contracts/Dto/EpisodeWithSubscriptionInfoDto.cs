namespace TvShowReminder.Contracts.Dto
{
    public class EpisodeWithSubscriptionInfoDto
    {
        public Subscription Subscription { get; set; }
        public Episode Episode { get; set; }
    }
}