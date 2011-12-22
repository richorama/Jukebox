using System;
using System.Collections.Concurrent;
using WMPLib;

namespace Jukebox
{
    static class Player
    {
        static Player()
        {
            queue = new ConcurrentQueue<Song>();
            wmp = new WMPLib.WindowsMediaPlayer();
            wmp.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(wmp_PlayStateChange);
        }

        static void wmp_PlayStateChange(int NewState)
        {
            if (NewState == 1)
            {
                Next();
            }
        }

        private static ConcurrentQueue<Song> queue { get; set; }

        private static WindowsMediaPlayer wmp { get; set; }

        public static Song[] Queue
        {
            get
            {
                return queue.ToArray();
            }
        }

        public static Song CurrentSong { get; private set; }

        private static void Play(Song song)
        {
            // update the song info
            song.PlayCount += 1;
            song.LastPlayed = DateTime.Now;

            // stop the media player
            wmp.controls.stop();
            wmp.PlayStateChange -= new _WMPOCXEvents_PlayStateChangeEventHandler(wmp_PlayStateChange);
            wmp.close();

            // create a new media player for the next song
            wmp = new WindowsMediaPlayer();
            wmp.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(wmp_PlayStateChange);
            wmp.URL = song.Location;
            wmp.controls.play();

            Console.WriteLine("Playing {0}", song.ToString());
        }

        private static void Next()
        {
            Song song = null;
            if (queue.TryDequeue(out song))
            {
                Play(song);
            }
            CurrentSong = song;
        }

        public static int Push(Song song)
        {
            queue.Enqueue(song);
            if (wmp.playState == WMPPlayState.wmppsStopped || wmp.playState == WMPPlayState.wmppsUndefined)
            {
                Next();
            }
            else
            {
                Console.WriteLine("Queued {0}", song.ToString());
            }
            return queue.Count;
        }
    }
}
