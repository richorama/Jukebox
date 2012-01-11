using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace Jukebox.Controllers
{
    public class Songs : NancyModule
    {
        public Songs()
        {
            Get["/Songs/{Filter}/{Value}"] = x =>
            {
                return QuerySongs(x.Filter, x.Value);
            };
            Get["/Songs/{Filter}"] = x =>
            {
                return QuerySongs(x.Filter, "");
            };
        }

        private Nancy.Response QuerySongs(string filter, string value)
        {
            IEnumerable<Jukebox.Song> query = Catalogue.Songs as IEnumerable<Jukebox.Song>;

            switch (filter)
            {
                case "Genre":
                    query = query.Where(s => string.Compare(s.Genre, value, false) == 0).OrderBy(s => s.Title);
                    break;
                case "Album":
                    query = query.Where(s => string.Compare(s.Album, value, false) == 0).OrderBy(s => s.Title);
                    break;
                case "Artist":
                    query = query.Where(s => string.Compare(s.Artist, value, false) == 0).OrderBy(s => s.Title);
                    break;
                case "Latest":
                    query = query.OrderByDescending(s => s.LastPlayed);
                    break;
                case "Popular":
                    query = query.Where(s => s.PlayCount > 0).OrderByDescending(s => s.PlayCount);
                    break;
                case "Queue":
                    query = Player.Queue;
                    break;
                case "Current":
                    var songs = new List<Jukebox.Song>();
                    if (null != Player.CurrentSong)
                    {
                        songs.Add(Player.CurrentSong);
                    }
                    query = songs.ToArray();
                    break;
                case "Search":
                    query = query.Where(s =>
                        Extensions.Contains(s.Artist, value, true)
                        || Extensions.Contains(s.Album, value, true)
                        || Extensions.Contains(s.Title, value, true)
                        || Extensions.Contains(s.Genre, value, true));
                    break;
                default:
                    query = query.OrderBy(s => s.Title);
                    break;
            }

            return Response.AsJson(query.ToArray());
        }
    }


}
