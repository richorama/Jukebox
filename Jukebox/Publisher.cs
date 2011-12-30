using System;
using System.Net;

namespace Jukebox
{
    static class Publisher
    {
        public static void Message(Song song)
        {

            string message = song == null ? "" : song.ToString();
            Action action = new Action(() =>
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8080/" + message);
                    using (request.GetResponse())
                    { }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });
            action.BeginInvoke(null, null);
        }

    }
}
