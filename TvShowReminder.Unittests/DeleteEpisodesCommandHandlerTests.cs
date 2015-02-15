using NSubstitute;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Command;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class DeleteEpisodesCommandHandlerTests
    {
        private readonly DeleteEpisodesCommandHandler _handler;
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;

        private readonly DeleteEpisodesCommand _command;
        private readonly int[] _episodeIds = { 1, 2, 3, 4 };

        public DeleteEpisodesCommandHandlerTests()
        {
            _command = new DeleteEpisodesCommand
            {
                EpisodeIds = _episodeIds
            };

            _episodeCommandDataSource = Substitute.For<IEpisodeCommandDataSource>();
            _handler = new DeleteEpisodesCommandHandler(_episodeCommandDataSource);    
        }

        [Fact]
        public void Should_delete_all_episodes()
        {
            _handler.Handle(_command);

            _episodeCommandDataSource.Received(1).DeleteEpisode(1);
            _episodeCommandDataSource.Received(1).DeleteEpisode(2);
            _episodeCommandDataSource.Received(1).DeleteEpisode(3);
            _episodeCommandDataSource.Received(1).DeleteEpisode(4);
        }

        [Fact]
        public void Should_not_perform_any_delete()
        {
            _handler.Handle(new DeleteEpisodesCommand { EpisodeIds = new int[0]});

            _episodeCommandDataSource.DidNotReceive().DeleteEpisode(Arg.Any<int>());
        }
    }
}
