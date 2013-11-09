using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using System.Web.Mvc;
using Messim.UI.Models;
using Image = Messim.UI.Models.Image;

namespace Messim.UI.Controllers
{
    public class MessageController : Controller
    {
        private MessimContext dbContext;

        public MessageController()
        {
            dbContext = new MessimContext();
        }

        //GET: //Message/ID
        public ActionResult Details(int id)
        {
            var msg = dbContext.Messages.First(x => x.ID == id);
            ViewData["DisplayMessages"] =
                dbContext.Messages.Where(x => x.ReplyTo != null && x.ReplyTo.ID == id).ToList();
            ViewData["Message"] = msg;

            return View();
        }

        //
        // POST: /Message/Send
        [HttpPost]
        [Authorize]
        public JsonResult Send(string messageText, HttpPostedFileBase messageImage)
        {
            SendMessageToDatabase(messageText, messageImage);

            return SuccessJsonResult();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reply(string messageText, HttpPostedFileBase messageImage, int messageId)
        {
            SendMessageToDatabase(messageText, messageImage, messageId);

            return SuccessJsonResult();
        }

        private JsonResult SuccessJsonResult()
        {
            return new JsonResult
                       {
                           ContentEncoding = Encoding.UTF8,
                           ContentType = "application/json; charset=UTF-8",
                           JsonRequestBehavior = JsonRequestBehavior.DenyGet
                       };
        }

        private void SendMessageToDatabase(string messageText, HttpPostedFileBase messageImage, int? replyTo = null)
        {
            string path = null;
            if (messageImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(messageImage.FileName);
                path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                messageImage.SaveAs(path);
            }
            var image = new Bitmap(path);
            var user = dbContext.Users.Single(x => x.Username == User.Identity.Name);
            var newImage = new Image
                               {
                                   URL = "/Content/uploads/" + Path.GetFileName(messageImage.FileName),
                                   Width = image.Width,
                                   Height = image.Height
                               };

            var newMessage = new Message
                                 {
                                     Text = messageText,
                                     Date = DateTime.Now,
                                     WhoLikes = new List<User>(),
                                     Sender = user,
                                     Image = newImage
                                 };

            if (replyTo != null)
            {
                var messageReplaingTo = dbContext.Messages.First(x => x.ID == replyTo);
                newMessage.ReplyTo = messageReplaingTo;
            }

            dbContext.Messages.Add(newMessage);
            dbContext.SaveChanges();
        }


        [HttpPost]
        public ActionResult Like(int messageId)
        {
            var message = dbContext.Messages.SingleOrDefault(x => x.ID == messageId);
            var personlikes = dbContext.Users.SingleOrDefault(x => x.Username == User.Identity.Name);
            if (message == null)
            {
                //FIXME: log error
                return new JsonResult() { Data = new { ConsoleMessage = "no such message in DB" } };
            }

            if (personlikes == null)
            {
                return new JsonResult() { Data = new { ConsoleMessage = "no such user in DB" } };
            }

            if (!message.WhoLikes.Contains(personlikes))
            {
                message.WhoLikes.Add(personlikes);
            }

            dbContext.Entry(message).State = EntityState.Modified;
            dbContext.SaveChanges();
            return new JsonResult { Data = new { ConsoleMessage = "Message ID: " + messageId + " was liked" } };
        }

        [HttpPost]
        public ActionResult Dislike(int messageId)
        {
            var message = dbContext.Messages.SingleOrDefault(x => x.ID == messageId);
            var personlikes = dbContext.Users.SingleOrDefault(x => x.Username == User.Identity.Name);
            if (message == null)
            {
                //FIXME: log error
                return new JsonResult() { Data = new { ConsoleMessage = "no such message in DB" } };
            }

            if (personlikes == null)
            {
                return new JsonResult() { Data = new { ConsoleMessage = "no such user in DB" } };
            }

            if (message.WhoLikes.Contains(personlikes))
            {
                message.WhoLikes.Remove(personlikes);
            }
            dbContext.Entry(message).State = EntityState.Modified;
            dbContext.SaveChanges();
            return new JsonResult { Data = new { ConsoleMessage = "Message with " + messageId + " unliked" } };
        }

        [HttpPost]
        public ActionResult CheckMessageStatus(int messageId)
        {
            var message = dbContext.Messages.SingleOrDefault(x => x.ID == messageId);
            var user = dbContext.Users.Single(x => x.Username == User.Identity.Name);

            bool currentUserLike = message.WhoLikes.Contains(user);
            int voteCount = message.WhoLikes.Count;
            return new JsonResult { Data = new { CurrentUserLike = currentUserLike, VoteCount = voteCount, ConsoleMessage = "Message with " + messageId + " unliked" } };
        }
    }
}