using System;
using System.Configuration;
using System.Net;
using Kayak;
using Kayak.Http;

namespace Jukebox
{
    class Program
    {
        const int port = 1337;

        private static string GetHome()
        {
            return (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        }

        static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["path"];
            string[] paths = null;
            if (string.IsNullOrWhiteSpace(path))
            {
                paths = new string[] { GetHome() };
            }
            else
            {
                paths = (path ?? "").Split(';');
            }

            Action a = new Action(() => { Catalogue.Index(paths); });
            a.BeginInvoke(null, null);

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
