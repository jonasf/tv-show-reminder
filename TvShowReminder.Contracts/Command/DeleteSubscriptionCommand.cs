namespace TvShowReminder.Contracts.Command
{
    public class DeleteSubscriptionCommand : ICommand
    {
        public int SubscriptionId { get; set; }
    }
}