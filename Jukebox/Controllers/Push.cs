using System.Linq;
using Nancy;

namespace Jukebox.Controllers
{
    public class Push : NancyModule
    {
        public Push()
        {
            Get["/push/{Id}"] = x =>
            {
                var song = (from s in Catalogue.Songs where s.Id == (string)x.Id select s).FirstOrDefault();
                if (null != song)
                {
                    return Player.Push(song).ToString();
                }
                return Response.AsJson("");
            };
        }

    }
}
