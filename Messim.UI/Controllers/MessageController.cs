using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Messim.UI.Models;
using Image = Messim.UI.Models.Image;

namespace Messim.UI.Controllers
{
    public class MessageController : Controller
    {


        //GET: //Message/<ID
        public ActionResult Details(int id)
        {
            var db = new MessimContext();

            var msg = db.Messages.First(x => x.ID == id);
            ViewData["DisplayMessages"] = db.Messages.Where(x => x.ReplyTo != null && x.ReplyTo.ID == id).ToList();
            ViewData["Message"] = msg;

            return View();
        }

        //
        // POST: /Message/Send
        [HttpPost]
        public JsonResult Send(string messageText, HttpPostedFileBase messageImage)
        {
            SendMessageToDatabase(messageText, messageImage);
            // Return JSON
            var result = new JsonResult { ContentEncoding = Encoding.UTF8, ContentType = "application/json; charset=UTF-8", JsonRequestBehavior = JsonRequestBehavior.DenyGet };
            return result;
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
            using (var db = new MessimContext())
            {
                var user = db.Users.Single(x => x.Username == User.Identity.Name);
                var newImage = new Image
                                   {
                                       URL = "/Content/uploads/" + Path.GetFileName(messageImage.FileName),
                                       Width = image.Width,
                                       Height = image.Height
                                   };

                Message newMessage;
                if (replyTo != null)
                {
                    var messageReplaingTo = db.Messages.First(x => x.ID == replyTo);
                    newMessage = new Message { Text = messageText, Date = DateTime.Now, LikeAmount = 0, Sender = user, Image = newImage, ReplyTo = messageReplaingTo };
                }
                else
                {
                    newMessage = new Message { Text = messageText, Date = DateTime.Now, LikeAmount = 0, Sender = user, Image = newImage };
                }
                db.Messages.Add(newMessage);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult Reply(string messageText, HttpPostedFileBase messageImage, int messageId)
        {
            Message messageReplaingTo = null;
            using (var db = new MessimContext())
            {

            }
            SendMessageToDatabase(messageText, messageImage, messageId);


            var result = new JsonResult { ContentEncoding = Encoding.UTF8, ContentType = "application/json; charset=UTF-8", JsonRequestBehavior = JsonRequestBehavior.DenyGet };
            return result;
        }

        [HttpPost]
        public ActionResult Like(int messageId)
        {
            using (var db = new MessimContext())
            {
                var message = db.Messages.SingleOrDefault(x => x.ID == messageId);

                if (message == null)
                {
                    //FIXME: log error
                    return new JsonResult() { Data = new { ConsoleMessage = "no such message in DB" } };
                }

                message.LikeAmount += 1;
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = new { ConsoleMessage = "Message with " + messageId + " liked" } };
            }
        }

        [HttpPost]
        public ActionResult Dislike(int messageId)
        {
            using (var db = new MessimContext())
            {
                var message = db.Messages.SingleOrDefault(x => x.ID == messageId);

                if (message == null)
                {
                    //FIXME: log error
                    return new JsonResult() { Data = new { ConsoleMessage = "no such message in DB" } };
                }
                if (message.LikeAmount > 0)
                {
                    message.LikeAmount -= 1;
                }
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = new { ConsoleMessage = "Message with " + messageId + " unliked" } };
            }
        }

    }

    public class FileUploadJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            this.ContentType = "text/html";
            context.HttpContext.Response.Write("<textarea>");
            base.ExecuteResult(context);
            context.HttpContext.Response.Write("</textarea>");
        }
    }
}
