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
        private readonly ICommandSender _commandSender;
        private readonly IQuerySender _querySender;

        public HomeController(IEpisodesCommandService episodesCommandService, 
                                ICommandSender commandSender,
                                IQuerySender querySender)
        {
            _episodesCommandService = episodesCommandService;
            _commandSender = commandSender;
            _querySender = querySender;
        }

        public ActionResult Index()
        {
            var result = _querySender.Send(new EpisodesToDateQuery {ToDate = DateTime.Now.AddDays(1)});
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