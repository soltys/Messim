using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Messim.UI.Models;

namespace Messim.UI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private MessimContext dbContext;

        public HomeController()
        {
            dbContext = new MessimContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var newMessages = dbContext.Messages.Where(x => x.ReplyTo == null).OrderByDescending(x => x.Date).Take(50).ToList();

            ViewData["DisplayMessages"] = newMessages;

            return View();
        }

        public ActionResult Best()
        {
            var newMessages =
                dbContext.Messages.Where(x => x.ReplyTo == null).OrderByDescending(x => x.LikeAmount).Take(50).ToList();

            ViewData["DisplayMessages"] = newMessages;

            return View();
        }
    }
}