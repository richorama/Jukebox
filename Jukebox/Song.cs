using System;

namespace Jukebox
{
    public class Song
    {
        public string Title { get; set; }

        public string Location { get; set; }

        public int PlayCount { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public DateTime? LastPlayed { get; set; }

        public override string ToString()
        {
            return string.Format("{0} by {1}", this.Title, this.Artist);
        }

        public string Id { get; set; }
    }
}
