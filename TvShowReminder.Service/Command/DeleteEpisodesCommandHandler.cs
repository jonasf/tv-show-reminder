using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service.Command
{
    public class DeleteEpisodesCommandHandler : ICommandHandler<DeleteEpisodesCommand>
    {
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;

        public DeleteEpisodesCommandHandler(IEpisodeCommandDataSource episodeCommandDataSource)
        {
            _episodeCommandDataSource = episodeCommandDataSource;
        }

        public void Handle(DeleteEpisodesCommand command)
        {
            foreach (var episodeId in command.EpisodeIds)
            {
                _episodeCommandDataSource.DeleteEpisode(episodeId);
            }
        }
    }
}
