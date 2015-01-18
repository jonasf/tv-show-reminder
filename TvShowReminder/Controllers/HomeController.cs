﻿using System;
using System.Linq;
using System.Web.Mvc;
using TvShowReminder.Model.Query;
using TvShowReminder.Models;
using TvShowReminder.Service;

namespace TvShowReminder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEpisodesCommandService _episodesCommandService;
        private readonly IEpisodesQueryService _episodesQueryService;

        public HomeController(IEpisodesCommandService episodesCommandService, 
                                IEpisodesQueryService episodesQueryService)
        {
            _episodesCommandService = episodesCommandService;
            _episodesQueryService = episodesQueryService;
        }

        public ActionResult Index()
        {
            var result = _episodesQueryService.GetEpisodesUpToDate(new EpisodesToDateQuery { ToDate = DateTime.Now.AddDays(1) });
            var viewModel = new EpisodeListViewModel
            {
                HasResults = result.Episodes.Any(),
                EpisodeList = result.Episodes
            };
            return View(viewModel);
        }

        public ActionResult UpdateAll()
        {
            _episodesCommandService.UpdateEpisodeList();
            return RedirectToAction("Index", "Home");
        }
    }
}