using System.Linq;
using MockApi.Client;
using Xunit;

namespace SimpleTest
{
    public class TestSample
    {
        [Fact]
        public async void MyWebTest()
        {
            // Arrange
            var path = "api/person/{id}/details";
            var mockApi = new MockApiClient("http://localhost:56729");
            await mockApi.Setup("GET", path).Returns("{id: {id}}");

            //Act - do something

            //Assert
            var calls = (await mockApi.Calls("GET", path)).ToList();
            Assert.Equal(1, calls.Count);
            Assert.Equal("api/person/12/details", calls[0].Path);
            Assert.Equal("test", calls[0].Body.SelectToken("request.accounts[0].id"));
        }
    }
}