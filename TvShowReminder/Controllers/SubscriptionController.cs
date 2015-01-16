using System.Web.Mvc;
using TvShowReminder.Model.Query;
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
            var result = _subscriptionQueryService.Search(new SearchTvShowQuery { Query = q });
            return View();
        }
    }
}