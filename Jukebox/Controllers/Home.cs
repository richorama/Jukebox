using System.IO;
using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/")]
    class Home : IController
    {
        public string Execute(HttpRequestHead head, dynamic queryString)
        {
            using (var file = new StreamReader("default.htm"))
            {
                return file.ReadToEnd();
            }
        }

    }
}
