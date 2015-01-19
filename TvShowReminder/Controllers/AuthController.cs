using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace TvShowReminder.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string password)
        {
            string passwordFromConfig = ConfigurationManager.AppSettings["MasterPassword"];

            if (password.Equals(passwordFromConfig))
            {
                FormsAuthentication.SetAuthCookie("Default", true);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Felaktigt lösenord!");
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Auth");
        }
    }
}