using System;
using System.Configuration;
using Nancy;

namespace Jukebox
{
    /*
    public class Module : NancyModule
    {
        public Module()
        {
            Get["/greet/{name}"] = x =>
            {
                return string.Concat("Hello ", x.name);
            };
        }
    }*/

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


            var nancyHost = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:1337"));
            Console.WriteLine("Web server started on port {0}", port);
            nancyHost.Start();
            Console.ReadLine();
        }
    }
}
