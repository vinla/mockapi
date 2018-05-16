using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MockApi.Client
{
    public class MockApiAction
    {
        private readonly string _host;
        private readonly string _path;

        public MockApiAction(string host, string path)
        {
            _host = host;
            _path = path;
        }

        public async Task Returns(string document)
        {            
            var uri = $"{_host}/_setup/{_path}";

            using(var client = new HttpClient())
            {
                var response = await client.PostAsync(uri, new StringContent(document, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task ReturnsJson(object json)
        {
            await Returns( JsonConvert.SerializeObject(json) );
        }
    }           
}