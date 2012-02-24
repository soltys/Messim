using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
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
        public FileUploadJsonResult Send(string message, HttpPostedFileBase fileInput)
        {
            string path = null;
            if (fileInput.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileInput.FileName);
                path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                fileInput.SaveAs(path);
            }
            var image = new Bitmap(path);
            using (var db = new MessimContext())
            {
                var user = db.Users.Single(x => x.Username == User.Identity.Name);
                var newImage = new Image { URL = "/Content/uploads/" + Path.GetFileName(fileInput.FileName), Width = image.Width, Height = image.Height };
                var newMessage = new Message { Text = message, Date = DateTime.Now, LikeAmount = 0, Sender = user, Image = newImage };
                db.Messages.Add(newMessage);
                db.SaveChanges();
            }
            // Return JSON
            return new FileUploadJsonResult { Data = new { message = string.Format("{0} uploaded successfully.", System.IO.Path.GetFileName(fileInput.FileName)) } };
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
