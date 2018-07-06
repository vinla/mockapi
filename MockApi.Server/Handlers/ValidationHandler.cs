using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;

namespace MockApi.Server.Handlers
{
    public class ValidationHandler : IRequestHandler
    {
        public string ProcessRequest(string method, PathString path, string bodyText)
        {
            var requestMethod = new HttpMethod(path.GetSegment(1));
            var requestPath = "/" + path.FromSegment(2);
            var routeSetup = DataCache.RouteSetups.SingleOrDefault(r => r.Path == requestPath && r.Method == requestMethod);

            if(routeSetup != null)
            {
                var responseObject = new 
                {
                    count = routeSetup.Requests.Count(),
                    requests = routeSetup.Requests.Select(rq => new 
                    {
                        path = rq.path,
                        body = rq.request
                    })
                };

                return Newtonsoft.Json.JsonConvert.SerializeObject(responseObject);
            }

            return string.Empty;
        }
    }
}