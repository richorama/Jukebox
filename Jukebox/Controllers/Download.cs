using System.IO;
using System.Linq;
using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/download.mp3", ContentType="audio/mpeg")]
    class Download : IController
    {
        public object Execute(HttpRequestHead head, dynamic queryString)
        {
            var song = Catalogue.Songs.Where(s => s.Id == queryString.Id).FirstOrDefault();

            using (var stream = new FileStream(song.Location, FileMode.Open))
            { 
                BinaryReader reader = new BinaryReader(stream);
                return reader.ReadBytes((int)stream.Length);
                
            }

        }

    }
}
