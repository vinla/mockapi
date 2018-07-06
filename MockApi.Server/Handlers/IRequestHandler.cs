using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace MockApi.Server.Handlers
{
    public interface IRequestHandler
    {
        string ProcessRequest(string method, PathString path, string bodyText);
    }
}