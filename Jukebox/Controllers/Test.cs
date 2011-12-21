using Kayak.Http;

namespace Jukebox.Controllers
{
    [Route(Url = "/test")]
    class Test : IController
    {
        public string Execute(HttpRequestHead head, dynamic queryString)
        {
            return queryString.Bar;
        }

    }

}
