using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace MockApi.Server.Handlers
{
    public class SetupHandler : IRequestHandler
    {
        public string ProcessRequest(string method, PathString path, string bodyAsText)
        {            
            var targetMethod = new HttpMethod(path.GetSegment(1));
            var requestPath = "/" + path.FromSegment(2);

            Console.WriteLine($"{path} - {targetMethod} - {requestPath}");

            DataCache.RouteSetups.RemoveAll(r => r.Path == requestPath && r.Method == targetMethod);
            DataCache.RouteSetups.Add(new RouteSetup(targetMethod, requestPath, bodyAsText));
            return path;					
        }
    }
}