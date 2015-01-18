using System.Web.Mvc;
using TvShowReminder.Service;

namespace TvShowReminder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEpisodesCommandService _episodesCommandService;

        public HomeController(IEpisodesCommandService episodesCommandService)
        {
            _episodesCommandService = episodesCommandService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateAll()
        {
            _episodesCommandService.UpdateEpisodeList();
            return RedirectToAction("Index", "Home");
        }
    }
}