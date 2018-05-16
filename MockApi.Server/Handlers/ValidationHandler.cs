using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MockApi.Server.Handlers
{
    public class ValidationHandler : IRequestHandler
    {
        public string ProcessRequest(PathString path, string bodyText)
        {
            var route = path.Value.Replace("/_validate", "");
            var routeSetup = DataCache.RouteSetups.SingleOrDefault(r => r.Route == route);

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