using System;
using System.Collections.Generic;
using Kayak;
using Kayak.Http;

namespace Jukebox
{
    class RequestDelegate : IHttpRequestDelegate
    {
        public void OnRequest(HttpRequestHead request, IDataProducer requestBody, IHttpResponseDelegate response)
        {
            try
            {
                Console.WriteLine(request.Uri);

                var content = Router.Execute(request);
                content = content ?? string.Empty;
                if (content.StartsWith("<"))
                {
                    Return200(response, content, "text/html");
                }
                else
                {
                    Return200(response, content, "text/plain");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Return200(response, ex.ToString(), "text/plain");
            }
        }

        private static void Return200(IHttpResponseDelegate response, string content, string contentType)
        {
            var headers = new HttpResponseHead()
            {
                Status = "200 OK",
                Headers = new Dictionary<string, string>() 
                    {
                        { "Content-Type", contentType },
                        { "Content-Length", content.Length.ToString() },
                    }
            };

            var body = new BufferedProducer(content);
            response.OnResponse(headers, body);
        }
    }


}
