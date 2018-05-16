using System.Collections.Generic;

namespace MockApi.Server
{
    public class DataCache
	{
		private static List<RouteSetup> _routeSetups = new List<RouteSetup>();

		public static List<RouteSetup> RouteSetups => _routeSetups;
	}	
}