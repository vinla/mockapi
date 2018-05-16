using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MockApi.Server.Handlers
{
    public class WebRequestHandler : IRequestHandler
    {
        public string ProcessRequest(PathString path, string bodyAsText)
        {
            var routeMatch = DataCache.RouteSetups.Select(r => r.MatchesOn(path))
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