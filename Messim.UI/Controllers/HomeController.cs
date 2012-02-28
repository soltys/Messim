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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            Message[] newMessages = null;
            var db = new MessimContext();

            newMessages = db.Messages.Where(x=> x.ReplyTo == null).OrderByDescending(x => x.Date).Take(50).ToArray();

            ViewData["DisplayMessages"] = newMessages;

            return View();
        }
        public ActionResult Best()
        {
            Message[] newMessages = null;
            var db = new MessimContext();

            newMessages = db.Messages.Where(x=> x.ReplyTo == null).OrderByDescending(x => x.LikeAmount).Take(50).ToArray();

            ViewData["DisplayMessages"] = newMessages;

            return View();
        }
    }
}
