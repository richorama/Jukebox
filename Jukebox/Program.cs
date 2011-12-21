using System;
using System.Net;
using Kayak;
using Kayak.Http;

namespace Jukebox
{
    class Program
    {
        const int port = 8080;

        static void Main(string[] args)
        {
            Catalogue.Index(new string[] { @"C:\Users\richard.astbury\Dropbox\Music", @"C:\Users\Public\Music\Sample Music" });
            Console.WriteLine("Indexed {0} songs", Catalogue.Songs.Count);

            var scheduler = KayakScheduler.Factory.Create(new SchedulerDelegate());
            scheduler.Post(() =>
            {
                KayakServer.Factory
                    .CreateHttp(new RequestDelegate(), scheduler)
                    .Listen(new IPEndPoint(IPAddress.Any, port));
            });

            Console.WriteLine("Web server started on port {0}", port);
            scheduler.Start();

        }
    }
}
