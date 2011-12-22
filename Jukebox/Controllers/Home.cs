using System.IO;
using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/", ContentType = "text/html")]
    class Home : IController
    {
        public object Execute(HttpRequestHead head, dynamic queryString)
        {
            using (var file = new StreamReader("default.htm"))
            {
                return file.ReadToEnd();
            }
        }

    }
}
