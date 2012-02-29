﻿
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
        readonly MembershipProvider provider = new MessimMembershipProvider();
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            var db = new MessimContext();

            var user = db.Users.Single(x => x.Username == User.Identity.Name);

            ViewData["PostNumber"] = db.Messages.Count(x => x.Sender.ID == user.ID);
            var userMessages = db.Messages.Where(x => x.Sender.ID == user.ID && x.ReplyTo == null).ToList();
            var subscipsionsMessages = (from msg in db.Messages
                                        from usr in user.Subscribents
                                        where msg.Sender.ID == usr.ID && msg.ReplyTo == null
                                        select msg).ToList();

            var messageUnion = userMessages.Union(subscipsionsMessages).ToList();
            messageUnion.Sort((x, y) => y.Date.CompareTo(x.Date));
            ViewData["DisplayMessages"] = messageUnion;
            ViewData["Sender"] = user.ID;


            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string username, string password, string passwordAgain)
        {
            if (!ValidateRegister(username, password, passwordAgain))
            {
                return View();
            }
            using (var _db = new MessimContext())
            {
                User newUser = new User { Username = username, Password = SHA.CreateSHA1Hash(password) };
                _db.Users.Add(newUser);
                _db.SaveChanges();
            }

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
            using (var _db = new MessimContext())
            {

                if (_db.Users.Any() && _db.Users.ToList().Any(x => x.Username == username))
                {
                    ModelState.AddModelError("username", "Nazwa użytkownika jest już zajęta");
                }

            }
            return ModelState.IsValid;
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
