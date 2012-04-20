using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Messim.UI.Authentication;
using Messim.UI.Models;
using Messim.Util;

namespace Messim.UI.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private readonly MembershipProvider provider = new MessimMembershipProvider();
        private MessimContext dbContext;

        public UserController()
        {
            dbContext = new MessimContext();
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            var user = dbContext.Users.Single(x => x.Username == User.Identity.Name);

            ViewData["PostNumber"] = dbContext.Messages.Count(x => x.Sender.ID == user.ID && x.ReplyTo == null);
            var userMessages = dbContext.Messages.Where(x => x.Sender.ID == user.ID && x.ReplyTo == null).ToList();

            userMessages.Sort((x, y) => y.Date.CompareTo(x.Date));
            ViewData["DisplayMessages"] = userMessages;
            ViewData["Sender"] = user.ID;

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string passwordAgain)
        {
            if (!ValidateRegister(username, password, passwordAgain))
            {
                return View();
            }
            User newUser = new User { Username = username, Password = SHA.CreateSHA1Hash(password) };
            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            ViewBag.Success = true;
            return View();
        }

        private bool ValidateRegister(string username, string password, string passwordAgain)
        {
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "Musisz podać nazwę użytkownika");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "Musisz podać hasło");
            }
            if (password != passwordAgain)
            {
                ModelState.AddModelError("_FORM", "Hasła muszą być takie same");
            }

            if (dbContext.Users.Any() && dbContext.Users.ToList().Any(x => x.Username == username))
            {
                ModelState.AddModelError("username", "Nazwa użytkownika jest już zajęta");
            }

            return ModelState.IsValid;
        }

        public ActionResult List(string username)
        {
            User user;
            try
            {
                user = dbContext.Users.Single(x => x.Username == username);
            }
            catch (Exception)
            {
                user = null;
                return RedirectToAction("NoUser");
            }
            if (user != null)
            {
                ViewData["PostNumber"] = dbContext.Messages.Count(x => x.Sender.ID == user.ID && x.ReplyTo == null);
                ViewData["Username"] = user.Username;
                var userMessages = dbContext.Messages.Where(x => x.Sender.ID == user.ID && x.ReplyTo == null).ToList();

                userMessages.Sort((x, y) => y.Date.CompareTo(x.Date));
                ViewData["DisplayMessages"] = userMessages;
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
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