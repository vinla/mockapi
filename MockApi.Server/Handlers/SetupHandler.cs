using Microsoft.AspNetCore.Http;

namespace MockApi.Server.Handlers
{
    public class SetupHandler : IRequestHandler
    {
        public string ProcessRequest(PathString path, string bodyAsText)
        {
            var route = path.Value.Replace("/_setup", "");
            DataCache.RouteSetups.RemoveAll(r => r.Route == route);
            DataCache.RouteSetups.Add(new RouteSetup(route, bodyAsText));
            return path;					
        }
    }
}