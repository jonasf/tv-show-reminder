namespace TvShowReminder.Contracts.Command
{
    public class DeleteEpisodesCommand : ICommand
    {
        public int[] EpisodeIds { get; set; }
    }
}
