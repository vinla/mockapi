using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockApi.Client
{
    public class MockApiCallDetails
    {
        private readonly string _path;
        private readonly JObject _body;

        public MockApiCallDetails(string path, JObject body)
        {
            _path = path;
            _body = body;
        }            

        public string Path => _path;

        public JObject Body => _body;
    }
}