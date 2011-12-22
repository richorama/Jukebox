using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jukebox
{
    public static class Catalogue
    {
        static Catalogue()
        {
            Songs = new ConcurrentBag<Song>();
        }

        public static void Index(string[] paths)
        {
            Parallel.ForEach<string>(GetFiles(paths), filename =>
            {
                TagLib.File file = TagLib.File.Create(filename);
                var song = new Song
                {
                    Title = file.Tag.Title ?? "Unknown Title",
                    Location = filename,
                    Artist = file.Tag.FirstAlbumArtist ?? "Unknown Artist",
                    Album = file.Tag.Album ?? "Unknown Album",
                    Genre = file.Tag.FirstGenre ?? "Unknown Genre",
                    Id = Guid.NewGuid().ToString(),
                };

                Songs.Add(song);
            });

            // index child directories
            Parallel.ForEach<string>(paths, path =>
            {
                try
                {
                    Index(Directory.GetDirectories(path));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });
        }

        private static IEnumerable<string> GetFiles(string[] paths)
        {
            foreach (string path in paths)
            {
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path, "*.mp3");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    continue;
                }
                foreach (var item in files)
                {
                    yield return item;
                }
            }
        }

        public static ConcurrentBag<Song> Songs { get; private set; }

    }
}
