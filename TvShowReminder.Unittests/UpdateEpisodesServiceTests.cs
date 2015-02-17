﻿using System;
using System.Collections.Generic;
using NSubstitute;
using NSubstitute.Core;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Command;
using TvShowReminder.TvRageApi;
using TvShowReminder.TvRageApi.Domain;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class UpdateEpisodesServiceTests
    {
        private readonly UpdateEpisodesService _service;

        private readonly IEpisodeCommandDataSource _episodeCommandDataSource;
        private readonly ITvRageService _tvRageService;

        private readonly Subscription _subscription1;
        private readonly Subscription _subscription2;

        private readonly TvRageEpisode _regularEpisode1;
        private readonly TvRageEpisode _regularEpisode2;
        private readonly TvRageEpisode _regularEpisode3;
        private readonly TvRageEpisode _regularEpisode4;
        private readonly TvRageEpisode _regularEpisode5;
        private readonly TvRageSpecialEpisode _specialEpisode1;
        private readonly TvRageSpecialEpisode _specialEpisode2;
        private readonly TvRageSpecialEpisode _specialEpisode3;

        public UpdateEpisodesServiceTests()
        {
            _subscription1 = CreateSubscription(1, 555, "The Stuff", DateTime.Now.AddDays(-2).Date);
            _subscription2 = CreateSubscription(2, 666, "The Stuff 2", DateTime.Now.AddDays(-3).Date);

            _regularEpisode1 = CreateRegularEpisode(2, 3, "Stuff happened", DateTime.Now.AddDays(2).Date);
            _regularEpisode2 = CreateRegularEpisode(2, 4, "Stuff didn't happen", DateTime.Now.AddDays(-1).Date);
            _regularEpisode3 = CreateRegularEpisode(1, 1, "Stuff 2 stuff", DateTime.Now.AddDays(-1).Date);
            _regularEpisode4 = CreateRegularEpisode(1, 2, "No stuff", DateTime.Now.AddDays(-3).Date);
            _regularEpisode5 = CreateRegularEpisode(1, 3, "Oh dear", DateTime.Now.AddDays(-4).Date);

            _specialEpisode1 = CreateSpecialEpisode(2, "Special stuff", DateTime.Now.AddDays(1).Date);
            _specialEpisode2 = CreateSpecialEpisode(1, "Stuff 2 Special", DateTime.Now.AddDays(-2).Date);
            _specialEpisode3 = CreateSpecialEpisode(1, "Stuff 2 Special 2", DateTime.Now.AddDays(-4).Date);

            _episodeCommandDataSource = Substitute.For<IEpisodeCommandDataSource>();
            _tvRageService = Substitute.For<ITvRageService>();
            _service = new UpdateEpisodesService(_episodeCommandDataSource, _tvRageService);
        }

        [Fact]
        public void Should_add_all_episodes_for_first_show()
        {
            _tvRageService.GetEpisodes(555).Returns(CreateEpisodeList1());

            _service.UpdateEpisodesForSubscription(_subscription1);

            _tvRageService.Received().GetEpisodes(555);
            _episodeCommandDataSource.Received().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 1 && s.SeasonNumber == 2 && s.EpisodeNumber == 3 && s.Title == "Stuff happened" && s.AirDate == DateTime.Now.AddDays(2).Date));
            _episodeCommandDataSource.Received().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 1 && s.SeasonNumber == 2 && s.EpisodeNumber == 4 && s.Title == "Stuff didn't happen" && s.AirDate == DateTime.Now.AddDays(-1).Date));
            _episodeCommandDataSource.Received().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 1 && s.SeasonNumber == 2 && s.EpisodeNumber == 0 && s.Title == "Special stuff" && s.AirDate == DateTime.Now.AddDays(1).Date));
        }

        [Fact]
        public void Should_add_some_of_the_episodes_for_the_second_show()
        {
            _tvRageService.GetEpisodes(666).Returns(CreateEpisodeList2());

            _service.UpdateEpisodesForSubscription(_subscription2);

            _tvRageService.Received().GetEpisodes(666);
            _episodeCommandDataSource.Received().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 2 && s.SeasonNumber == 1 && s.EpisodeNumber == 1 && s.Title == "Stuff 2 stuff" && s.AirDate == DateTime.Now.AddDays(-1).Date));
            _episodeCommandDataSource.Received().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 2 && s.SeasonNumber == 1 && s.EpisodeNumber == 0 && s.Title == "Stuff 2 Special" && s.AirDate == DateTime.Now.AddDays(-2).Date));

            _episodeCommandDataSource.DidNotReceive().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 2 && s.SeasonNumber == 1 && s.EpisodeNumber == 2 && s.Title == "No stuff" && s.AirDate == DateTime.Now.AddDays(-3).Date));
            _episodeCommandDataSource.DidNotReceive().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 2 && s.SeasonNumber == 1 && s.EpisodeNumber == 3 && s.Title == "Oh dear" && s.AirDate == DateTime.Now.AddDays(-4).Date));
            _episodeCommandDataSource.DidNotReceive().SaveEpisode(Arg.Is<Episode>(s => s.SubscriptionId == 2 && s.SeasonNumber == 1 && s.EpisodeNumber == 3 && s.Title == "Stuff 2 Special 2" && s.AirDate == DateTime.Now.AddDays(-4).Date));
        }

        private TvRageSpecialEpisode CreateSpecialEpisode(int season, string title, DateTime airdate)
        {
            return new TvRageSpecialEpisode { Season = season, Title = title, AirDate = airdate };
        }

        private TvRageEpisode CreateRegularEpisode(int seasonNumber, int episodeNumber, string title, DateTime airDate)
        {
            return new TvRageEpisode { Season = seasonNumber, SeasonNum = episodeNumber, Title = title, AirDate = airDate };
        }

        private Subscription CreateSubscription(int id, int tvShowId, string tvShowName, DateTime lastAirDate)
        {
            return new Subscription { Id = id, TvShowId = tvShowId, TvShowName = tvShowName, LastAirDate = lastAirDate };
        }

        private TvRageEpisodeList CreateEpisodeList1()
        {
            return new TvRageEpisodeList
            {
                Episodes = new List<TvRageEpisode> { _regularEpisode1, _regularEpisode2 },
                SpecialEpisodes = new List<TvRageSpecialEpisode> { _specialEpisode1 }
            };
        }

        private TvRageEpisodeList CreateEpisodeList2()
        {
            return new TvRageEpisodeList
            {
                Episodes = new List<TvRageEpisode> { _regularEpisode3, _regularEpisode4, _regularEpisode5 },
                SpecialEpisodes = new List<TvRageSpecialEpisode> { _specialEpisode2, _specialEpisode3 }
            };
        }
    }
}
