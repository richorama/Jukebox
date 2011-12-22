using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/list")]
    class List : IController
    {
        public object Execute(HttpRequestHead head, dynamic queryString)
        {
            IEnumerable<string> query = null;
            switch ((string)queryString.Filter)
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

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(query.OrderBy(s => s).ToArray());
        }

    }
}
