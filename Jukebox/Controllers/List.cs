using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Nancy;

namespace Jukebox.Controllers
{
    public class List : NancyModule
    {
        public List()
        {
            Get["/list/{Filter}"] = x => {
                IEnumerable<string> query = null;
                switch ((string)x.Filter)
                {
                    case "Artist":
                        query = (from s in Catalogue.Songs where s.Artist != null select s.Artist).Distinct();
                        break;
                    case "Album":
                        query = (from s in Catalogue.Songs where s.Album != null select s.Album).Distinct();
                        break;
                    case "Genre":
                        query = (from s in Catalogue.Songs where s.Genre != null select s.Genre).Distinct();
                        break;
                }
                return Response.AsJson(query.OrderBy(s => s).ToArray());
            };
        }

    }
}
