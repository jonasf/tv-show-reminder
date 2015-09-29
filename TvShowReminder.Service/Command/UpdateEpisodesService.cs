using System;
using System.Collections.Generic;
using System.Linq;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.DataSource;
using TvShowReminder.TvMazeApi;
using TvShowReminder.TvMazeApi.Domain;

namespace TvShowReminder.Service.Command
{
    public class UpdateEpisodesService : IUpdateEpisodesService
    {
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;
        private readonly ITvMazeService _tvMazeService;

        public UpdateEpisodesService(IEpisodeCommandDataSource episodeCommandDataSource, ITvMazeService tvMazeService)
        {
            _episodeCommandDataSource = episodeCommandDataSource;
            _tvMazeService = tvMazeService;
        }

        public DateTime? UpdateEpisodesForSubscription(Subscription subscription)
        {
            var episodeList = _tvMazeService.GetEpisodes(subscription.TvShowId);

            var newEpisodes = CreateEpisodes(episodeList, subscription.Id, subscription.LastAirDate);
            SaveEpisodes(newEpisodes);

            return GetLastAirDateForNewEpisodes(newEpisodes);
        }

        private DateTime? GetLastAirDateForNewEpisodes(IList<Episode> newEpisodes)
        {
            DateTime? lastAirDateForNewEpisodes = null;

            if (newEpisodes.Any())
                lastAirDateForNewEpisodes = newEpisodes.OrderByDescending(s => s.AirDate).Take(1).First().AirDate;

            return lastAirDateForNewEpisodes;
        }

        private void SaveEpisodes(IEnumerable<Episode> episodes)
        {
            foreach (var episode in episodes)
            {
                _episodeCommandDataSource.SaveEpisode(episode);
            }
        }

        private List<Episode> CreateEpisodes(IEnumerable<TvMazeEpisode> episodes, int subscriptionId, DateTime lastAirDate)
        {
            return episodes
                .Where(s => s.AirDate > lastAirDate)
                .Select(episode => new Episode
                {
                    SubscriptionId = subscriptionId,
                    SeasonNumber = episode.Season,
                    EpisodeNumber = episode.Number,
                    AirDate = episode.AirDate.Date,
                    Title = episode.Name
                }).ToList();
        }
    }
}
