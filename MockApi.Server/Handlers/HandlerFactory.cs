using System;
using Microsoft.AspNetCore.Http;

namespace MockApi.Server.Handlers
{
    public static class HandlerFactory
    {
        public static IRequestHandler GetHandler(PathString path)
        {
            var routeStart = path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
            switch(routeStart)
            {
                case "_setup":
                    return new SetupHandler();
                case "_validate":
                    return new ValidationHandler();
                default:
                    return new WebRequestHandler();
            }
        }
    }
}