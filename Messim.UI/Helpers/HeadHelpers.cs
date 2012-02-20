using System.Web.Mvc;
using System.IO;
namespace Messim.UI.Helpers
{

    public static class HeadLoaders
    {
        private const string ContentRoot = "~/";
        public static MvcHtmlString LoadJS(this UrlHelper urlHelper, string fileName)
        {
            const string javascriptFolder = "Content/js";
            string filePath = Path.Combine(ContentRoot, javascriptFolder, fileName);
            return new MvcHtmlString("<script src=\"" + urlHelper.Content(filePath) + "\" type=\"text/javascript\"></script>");
        }
        public static MvcHtmlString LoadCSS(this UrlHelper urlHelper, string fileName)
        {
            const string cssFolder = "Content/less";
            string filePath = Path.Combine(ContentRoot, cssFolder, fileName);
            return new MvcHtmlString("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + urlHelper.Content(filePath) + "\" />");
        }
    }
}