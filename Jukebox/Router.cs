using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Kayak.Http;

namespace Jukebox
{

    class Route : Attribute
    {
        public string Url { get; set; }
    }

    interface IController
    {
        string Execute(HttpRequestHead head, dynamic queryString);
    }

    static class Router
    {

        static Dictionary<string, IController> controllers = new Dictionary<string, IController>();

        static Router()
        {
            var types = (from t in typeof(Router).Assembly.GetTypes()
                         where t.GetCustomAttributes(typeof(Route), false).Length > 0
                         where typeof(IController).IsAssignableFrom(t)
                         select t);

            foreach (var type in types)
            {
                var controller = Activator.CreateInstance(type) as IController;
                Route route = type.GetCustomAttributes(typeof(Route), false).First() as Route;
                controllers.Add(route.Url.ToLower(), controller);
            }
        }

        public static string Execute(HttpRequestHead head)
        {
            string path = (head.Uri ?? "").Split('?').First().ToLower();

            dynamic queryVariable = new ExpandoObject();

            if ((head.Uri ?? "").Split('?').Length > 1)
            {
                string queryString = (head.Uri ?? "").Split('?')[1];
                string[] queries = queryString.Split('&');
                foreach (var query in queries)
                {
                    var items = query.Split('=');
                    if (items.Length != 2)
                        continue;

                    ((IDictionary<string, Object>)queryVariable)[items[0]] = System.Web.HttpUtility.UrlDecode(items[1]);
                }

            }
            if (controllers.ContainsKey(path))
            {
                var controller = controllers[path];
                return controller.Execute(head, queryVariable);
            }
            return null;
        }


    }
}
