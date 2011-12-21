using System.Linq;
using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/push")]
    class Push : IController
    {
        public string Execute(HttpRequestHead head, dynamic queryString)
        {
            var song = (from s in Catalogue.Songs where s.Id == queryString.Id select s).FirstOrDefault();
            if (null != song)
            {
                return Player.Push(song).ToString();
            }
            return "";
        }

    }
}
