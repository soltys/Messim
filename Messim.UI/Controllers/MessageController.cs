using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Messim.UI.Models;
using Image = Messim.UI.Models.Image;

namespace Messim.UI.Controllers
{
    public class MessageController : Controller
    {
        //
        // POST: /Message/Send
        [HttpPost]
        public JsonResult Send(string messageText, HttpPostedFileBase messageImage)
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
                var newImage = new Image { URL = "/Content/uploads/" + Path.GetFileName(messageImage.FileName), Width = image.Width, Height = image.Height };
                var newMessage = new Message { Text = messageText, Date = DateTime.Now, LikeAmount = 0, Sender = user, Image = newImage };
                db.Messages.Add(newMessage);
                db.SaveChanges();
            }
            // Return JSON
            return new JsonResult { Data = new { Msg = "Success" } };
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
