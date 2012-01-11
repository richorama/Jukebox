using System.Linq;
using Nancy;

namespace Jukebox.Controllers
{
    public class Song : NancyModule
    {
        public Song()
        {
            Get["/song/{Id}"] = x =>
            {
                var song = Catalogue.Songs.Where(s => s.Id == (string)x.Id).FirstOrDefault();
                return Response.AsJson(song);
            };
        }

    }
}
