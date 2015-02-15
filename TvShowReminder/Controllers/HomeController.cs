using System;
using System.Linq;
using System.Web.Mvc;
using TvShowReminder.Contracts.Command;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Framework;
using TvShowReminder.Models;
using TvShowReminder.Service;

namespace TvShowReminder.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEpisodesCommandService _episodesCommandService;
        private readonly IEpisodesQueryService _episodesQueryService;
        private readonly ICommandSender _commandSender;

        public HomeController(IEpisodesCommandService episodesCommandService, 
                                IEpisodesQueryService episodesQueryService,
                                ICommandSender commandSender)
        {
            _episodesCommandService = episodesCommandService;
            _episodesQueryService = episodesQueryService;
            _commandSender = commandSender;
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

        [HttpPost]
        public ActionResult Delete(int[] episodeIds)
        {
            if (episodeIds != null)
            {
                var command = new DeleteEpisodesCommand {EpisodeIds = episodeIds};
                _commandSender.Send(command);
            }
            return RedirectToAction("Index", "Home");       
        }

        public ActionResult UpdateAll()
        {
            _episodesCommandService.UpdateEpisodeList();
            return RedirectToAction("Index", "Home");
        }
    }
}