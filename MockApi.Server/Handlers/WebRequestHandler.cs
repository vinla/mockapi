using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;

namespace MockApi.Server.Handlers
{
    public class WebRequestHandler : IRequestHandler
    {
        public string ProcessRequest(string method, PathString path, string bodyAsText)
        {
            var requestMethod = new HttpMethod(method);
            var routeMatch = DataCache.RouteSetups.Select(r => r.MatchesOn(requestMethod, path))
							.FirstOrDefault(rm => rm.Success);

            if(routeMatch != null)
            {                
                routeMatch.Setup.LogRequest(path, bodyAsText);
                return  routeMatch.GetResponse(bodyAsText);
            }		

            return string.Empty;
        }
    }
}