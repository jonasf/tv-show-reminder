using System.Linq;
using System.Web.Mvc;
using TvShowReminder.Model.Query;
using TvShowReminder.Models;
using TvShowReminder.Service;

namespace TvShowReminder.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionQueryService _subscriptionQueryService;

        public SubscriptionController(ISubscriptionQueryService subscriptionQueryService)
        {
            _subscriptionQueryService = subscriptionQueryService;
        }

        public ActionResult Search(string q)
        {
            var searchViewModel = new SearchViewModel();

            if (HasSearchParameters(q))
            {
                var result = _subscriptionQueryService.Search(new SearchTvShowQuery { Query = q });
                searchViewModel.SearchWords = q;
                searchViewModel.HasSearch = true;
                searchViewModel.TvShows = result.TvShows;
                searchViewModel.SearchHits = result.TvShows.Count();
            }

            return View(searchViewModel);
        }

        private bool HasSearchParameters(string query)
        {
            return !string.IsNullOrEmpty(query);
        }
    }
}