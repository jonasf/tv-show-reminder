using System;
using System.Collections.Generic;
using System.Linq;
using TvShowReminder.Contracts.Command;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.DataSource;
using TvShowReminder.TvRageApi;
using TvShowReminder.TvRageApi.Domain;

namespace TvShowReminder.Service
{
    public class EpisodesCommandService : IEpisodesCommandService
    {
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;
        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;
        private readonly ITvRageService _tvRageService;

        public EpisodesCommandService(ISubscriptionQueryDataSource subscriptionQueryDataSource, ISubscriptionCommandDataSource subscriptionCommandDataSource, IEpisodeCommandDataSource episodeCommandDataSource, ITvRageService tvRageService)
        {
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
            _subscriptionCommandDataSource = subscriptionCommandDataSource;
            _episodeCommandDataSource = episodeCommandDataSource;
            _tvRageService = tvRageService;
        }

        public void UpdateEpisodeList()
        {
            var subscriptions = _subscriptionQueryDataSource.GetAllSubscriptions();
            foreach (var subscription in subscriptions)
            {
                var lastAirDate = UpdateEpisodesForSubscription(subscription);
                if(lastAirDate != null)
                    _subscriptionCommandDataSource.SaveLastAirDate(subscription.Id, lastAirDate.Value.Date);
            }
        }

        public void RefreshEpisodes(RefreshEpisodesCommand command)
        {
            var subscription = _subscriptionQueryDataSource.GetSubscription(command.SubscriptionId);
            subscription.LastAirDate = DateTime.Now.Date;

            _episodeCommandDataSource.DeleteAllFromSubscription(command.SubscriptionId);
           
            var lastAirDate = UpdateEpisodesForSubscription(subscription);
            if (lastAirDate != null)
                _subscriptionCommandDataSource.SaveLastAirDate(subscription.Id, lastAirDate.Value.Date);
        }

        private DateTime? UpdateEpisodesForSubscription(Subscription subscription)
        {
            var episodeList = _tvRageService.GetEpisodes(subscription.TvShowId);
            
            var newRegularEpisodes = GetNewRegularEpisodes(episodeList.Episodes, subscription.Id, subscription.LastAirDate);
            var newSpecialEpisodes = GetNewSpecialEpisodes(episodeList.SpecialEpisodes, subscription.Id, subscription.LastAirDate);

            SaveEpisodes(newRegularEpisodes);
            SaveEpisodes(newSpecialEpisodes);

            return GetLastAirDateForNewShows(newRegularEpisodes, newSpecialEpisodes);
        }

        private DateTime? GetLastAirDateForNewShows(IList<Episode> newRegularEpisodes, List<Episode> newSpecialEpisodes)
        {
            DateTime? regularShowsLastDate = null;
            DateTime? specialShowsLastDate = null;

            if (newRegularEpisodes.Any())
                regularShowsLastDate = newRegularEpisodes.OrderByDescending(s => s.AirDate).Take(1).First().AirDate;

            if (newSpecialEpisodes.Any())
                specialShowsLastDate = newSpecialEpisodes.OrderByDescending(s => s.AirDate).Take(1).First().AirDate;

            if (regularShowsLastDate == null && specialShowsLastDate == null)
            {
                return null;
            }
            if (specialShowsLastDate == null)
            {
                return regularShowsLastDate;
            }
            if (regularShowsLastDate == null)
            {
                return specialShowsLastDate;
            }
            if (regularShowsLastDate > specialShowsLastDate)
            {
                return regularShowsLastDate;
            }

            return specialShowsLastDate;
        }

        private void SaveEpisodes(IEnumerable<Episode> episodes)
        {
            foreach (var episode in episodes)
            {
                _episodeCommandDataSource.SaveEpisode(episode);
            }
        }

        private List<Episode> GetNewSpecialEpisodes(IEnumerable<TvRageSpecialEpisode> specialEpisodes, int subscriptionId, DateTime lastAirDate)
        {
            return specialEpisodes
                .Where(s => s.AirDate > lastAirDate)
                .Select(specialEpisode => new Episode
                {
                    SubscriptionId = subscriptionId,
                    SeasonNumber = specialEpisode.Season,
                    EpisodeNumber = 0,
                    AirDate = specialEpisode.AirDate.Date,
                    Title = specialEpisode.Title
                }).ToList();
        }

        private List<Episode> GetNewRegularEpisodes(IEnumerable<TvRageEpisode> episodes, int subscriptionId, DateTime lastAirDate)
        {
            return episodes
                .Where(s => s.AirDate > lastAirDate)
                .Select(episode => new Episode
                {
                    SubscriptionId = subscriptionId,
                    SeasonNumber = episode.Season,
                    EpisodeNumber = episode.SeasonNum,
                    AirDate = episode.AirDate.Date,
                    Title = episode.Title
                }).ToList();
        }
    }
}
