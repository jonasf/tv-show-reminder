using System.Linq;
using System.Web.Mvc;
using TvShowReminder.DatabaseMigrations;
using TvShowReminder.Model.Command;
using TvShowReminder.Model.Query;
using TvShowReminder.Models;
using TvShowReminder.Service;

namespace TvShowReminder.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionQueryService _subscriptionQueryService;
        private readonly ISubscriptionCommandService _subscriptionCommandService;

        public SubscriptionController(ISubscriptionQueryService subscriptionQueryService, 
                                        ISubscriptionCommandService subscriptionCommandService)
        {
            _subscriptionQueryService = subscriptionQueryService;
            _subscriptionCommandService = subscriptionCommandService;
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

        public ActionResult Add(int showId, string showName)
        {
            var command = new AddSubscriptionCommand
            {
                ShowId = showId,
                ShowName = showName
            };

            _subscriptionCommandService.AddSubscription(command);

            return View();
        }

        [HttpPost]
        public ActionResult Delete(int subscriptionId)
        {
            var command = new DeleteSubscriptionCommand
            {
                SubscriptionId = subscriptionId
            };

            _subscriptionCommandService.DeleteSubscription(command);

            return View();
        }

        public ActionResult List()
        {
            var result = _subscriptionQueryService.GetAll();
            var viewModel = new SubscriptionsListViewModel
            {
                Subscriptions = result.Subscriptions.OrderBy(s => s.TvShowName)
            };

            return View(viewModel);
        }

        private bool HasSearchParameters(string query)
        {
            return !string.IsNullOrEmpty(query);
        }
    }
}