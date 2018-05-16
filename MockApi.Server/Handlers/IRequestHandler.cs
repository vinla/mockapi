using Microsoft.AspNetCore.Http;

namespace MockApi.Server.Handlers
{
    public interface IRequestHandler
    {
        string ProcessRequest(PathString path, string bodyText);
    }
}