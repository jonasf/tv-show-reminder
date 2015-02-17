namespace TvShowReminder.Contracts.Command
{
    public class RefreshEpisodesCommand : ICommand
    {
        public int SubscriptionId { get; set; }
    }
}
