namespace TvShowReminder.Contracts.Command
{
    public class AddSubscriptionCommand : ICommand
    {
        public int ShowId { get; set; }
        public string ShowName { get; set; }
    }
}
