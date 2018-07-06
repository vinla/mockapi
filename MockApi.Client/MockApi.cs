using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockApi.Client
{
    public class MockApiClient
    {
        private readonly string _mockApiHost;

        public MockApiClient(string mockApiHost)
        {
            _mockApiHost = mockApiHost;
        }

        public MockApiAction Setup(string method, string path)
        {
            return new MockApiAction(_mockApiHost, method, path);
        }                

        public async Task<IEnumerable<MockApiCallDetails>> Calls(string method, string path)
        {
            var uri = $"{_mockApiHost}/_validate/{method}/{path}";

            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var bodyAsString = await response.Content.ReadAsStringAsync();
                var bodyAsJson = JObject.Parse(bodyAsString);

                var requests = bodyAsJson.SelectToken("requests") as JArray;
                var results = new List<MockApiCallDetails>();

                foreach (var request in requests.Children())
                {
                    JObject body = null;
                    // var temp = request["body"].ToString();
                    // if(string.IsNullOrEmpty(temp))
                    //     body = JObject.Parse(temp);
                    results.Add(new MockApiCallDetails(request["path"].ToString(), body));
                }

                return results;
            }

            throw new NotImplementedException();
        }
    }   
   
    public class Test
    {
        
    }
}
