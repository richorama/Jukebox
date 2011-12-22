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

                string contentType = null;
                var content = Router.Execute(request, out contentType) ?? string.Empty;
                Return200(response, content, contentType ?? "text/plain");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Return200(response, ex.ToString(), "text/plain");
            }
        }

        private static void Return200(IHttpResponseDelegate response, object content, string contentType)
        {
            var headers = new HttpResponseHead()
            {
                Status = "200 OK",
                Headers = new Dictionary<string, string>() 
                    {
                        { "Content-Type", contentType },
                        { "Content-Length", (content is string) ? ((string) content).Length.ToString() : ((byte[]) content).Length.ToString() },
                    }
            };


            BufferedProducer body = null;
            if (content is string)
            {
                body = new BufferedProducer(content as string);
            }
            else
            {
                body = new BufferedProducer(content as byte[]);
            }

            response.OnResponse(headers, body);
        }
    }


}
