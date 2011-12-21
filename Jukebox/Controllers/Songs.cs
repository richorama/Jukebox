﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/songs")]
    class Songs : IController
    {
        public string Execute(HttpRequestHead head, dynamic queryString)
        {
            IEnumerable<Jukebox.Song> query = Catalogue.Songs as IEnumerable<Jukebox.Song>;

            var dictionary = queryString as IDictionary<string, object>;
            if (dictionary.ContainsKey("Filter"))
            {
                switch ((string)queryString.Filter)
                {
                    case "Genre":
                        query = query.Where(s => string.Compare(s.Genre, queryString.Value, false) == 0).OrderBy(s => s.Title);
                        break;
                    case "Album":
                        query = query.Where(s => string.Compare(s.Album, queryString.Value, false) == 0).OrderBy(s => s.Title);
                        break;
                    case "Artist":
                        query = query.Where(s => string.Compare(s.Artist, queryString.Value, false) == 0).OrderBy(s => s.Title);
                        break;
                    case "Latest":
                        query = query.OrderByDescending(s => s.LastPlayed);
                        break;
                    case "Popular":
                        query = query.OrderByDescending(s => s.PlayCount);
                        break;
                    default:
                        query = query.OrderBy(s => s.Title);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(s => s.Title);
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(query.ToArray());
        }

    }
}