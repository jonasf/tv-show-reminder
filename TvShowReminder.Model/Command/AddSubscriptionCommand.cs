namespace TvShowReminder.Model.Command
{
    public class AddSubscriptionCommand
    {
        public int ShowId { get; set; }
        public string ShowName { get; set; }
    }
}
