using System;
using System.Collections.Generic;
using System.Linq;

namespace MockApi.Server
{
public class RouteSetup
	{
		private readonly string _route;
		private readonly string _response;
		private readonly List<(string path, string request)> _requests;

		public RouteSetup(string route, string response)
		{
			_route = route;
			_response = response;
			_requests = new List<(string, string)>();
		}

		public string Route => _route;

		public string Response => _response;

		public IEnumerable<(string path, string request)> Requests => _requests.ToList();

		public void LogRequest(string path, string request)
		{
			_requests.Add((path, request));
		}

		public RouteMatch MatchesOn(string requestPath)
		{
			var routeParts = _route.Split('/');
			var requestParts = requestPath.Split('/');
			var wildcards = new Dictionary<string, string>();

			if(routeParts.Length == requestParts.Length)
			{
				for(int i = 0; i < routeParts.Length; i++)
				{
					var routePart = routeParts[i];
					var requestPart = requestParts[i];

					if (routePart.StartsWith('{'))
					{
						var wildcardKey = routePart.Substring(1, routePart.Length - 2);
						wildcards.Add(wildcardKey, requestPart);
					}
					else if (routePart != requestPart)
						return RouteMatch.NoMatch;
				}

				return new RouteMatch(this, wildcards);
			}

			return RouteMatch.NoMatch;
		}		
	}
}