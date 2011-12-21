using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jukebox
{
    public static class Catalogue
    {
        public static void Index(string[] paths)
        {
            Songs = new ConcurrentBag<Song>();
            Parallel.ForEach<string>(GetFiles(paths), filename =>
            {
                TagLib.File file = TagLib.File.Create(filename);
                var song = new Song
                {
                    Title = file.Tag.Title,
                    Location = filename,
                    Artist = file.Tag.FirstAlbumArtist,
                    Album = file.Tag.Album,
                    Genre = file.Tag.FirstGenre,
                    Id = Guid.NewGuid().ToString(),
                };

                Songs.Add(song);
            });


        }

        private static IEnumerable<string> GetFiles(string[] paths)
        {
            foreach (string path in paths)
            {
                foreach (var item in Directory.GetFiles(path, "*.mp3"))
                {
                    yield return item;
                }
            }
        }

        public static ConcurrentBag<Song> Songs { get; private set; }

    }
}
