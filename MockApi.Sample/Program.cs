using System;
using System.Linq;
using System.Threading.Tasks;
using MockApi.Client;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Run(async () =>
            {
                Console.WriteLine("Starting test");

                var path = "api/person/{id}/details";
                var mockApi = new MockApiClient("http://localhost:8080");
                await mockApi.Setup(path).Returns("{id: {id}}");

                // Perfom an action
                Console.ReadLine();

                var calls = (await mockApi.Calls(path)).ToList();

                Console.WriteLine(calls.Count);  // Assert the number of calls made to the end point              
                Console.WriteLine(calls[0].Path); // Assert the actual path (for checking url params)
                Console.WriteLine(calls[0].Body.SelectToken("request.accounts[0].id")); // Assert information sent in the request
                
                Console.ReadLine();
                });

                task.Wait();
                Console.WriteLine("Test complete");
        }
    }
}
