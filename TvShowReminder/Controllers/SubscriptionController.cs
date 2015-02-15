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
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionQueryService _subscriptionQueryService;
        private readonly ISubscriptionCommandService _subscriptionCommandService;
        private readonly IEpisodesCommandService _episodesCommandService;
        private readonly ICommandSender _commandSender;
        private readonly IQuerySender _querySender;

        public SubscriptionController(ISubscriptionQueryService subscriptionQueryService, 
                                        ISubscriptionCommandService subscriptionCommandService,
                                        IEpisodesCommandService episodesCommandService,
                                        ICommandSender commandSender,
                                        IQuerySender querySender)
        {
            _subscriptionQueryService = subscriptionQueryService;
            _subscriptionCommandService = subscriptionCommandService;
            _episodesCommandService = episodesCommandService;
            _commandSender = commandSender;
            _querySender = querySender;
        }

        public ActionResult Search(string q)
        {
            var searchViewModel = new SearchViewModel();

            if (HasSearchParameters(q))
            {
                var result = _querySender.Send(new SearchTvShowQuery { Query = q });
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

            _commandSender.Send(command);

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

        [HttpPost]
        public ActionResult RefreshEpisodesForSubscription(int subscriptionId)
        {
            var command = new RefreshEpisodesCommand
            {
                SubscriptionId = subscriptionId
            };
            _episodesCommandService.RefreshEpisodes(command);

            return RedirectToAction("List", "Subscription");
        }

        public ActionResult List()
        {
            var result = _subscriptionQueryService.GetAllWithNextEpisode();
            var viewModel = new SubscriptionsListViewModel
            {
                Subscriptions = result.Subscriptions.OrderBy(s => s.Subscription.TvShowName)
            };

            return View(viewModel);
        }

        private bool HasSearchParameters(string query)
        {
            return !string.IsNullOrEmpty(query);
        }
    }
}