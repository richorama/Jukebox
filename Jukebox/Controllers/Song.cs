using System.Linq;
using System.Web.Script.Serialization;
using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/song")]
    class Song : IController
    {
        public string Execute(HttpRequestHead head, dynamic queryString)
        {
            var song = Catalogue.Songs.Where(s => s.Id == queryString.Id).FirstOrDefault();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(song);
        }

    }
}
