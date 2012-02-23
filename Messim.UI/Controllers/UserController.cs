using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Messim.UI.Authentication;
using Messim.UI.Models;

namespace Messim.UI.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        MembershipProvider provider = new MessimMembershipProvider();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {

            return View(new Models.MessimContext().Users.Select(x => x));
        }

        public ActionResult Login()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string username, string password, bool rememberUser, string returnUrl)
        {
            if (!ValidateLogOn(username, password))
            {
                return View();
            }

            FormsAuthentication.SetAuthCookie(username, createPersistentCookie: rememberUser);
            if (!String.IsNullOrEmpty(returnUrl) && returnUrl != "/")
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private bool ValidateLogOn(string username, string password)
        {
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!provider.ValidateUser(username, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }
            return ModelState.IsValid;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
