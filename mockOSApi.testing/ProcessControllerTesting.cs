using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using mockOSApi.Data;
using System.Text;


namespace mockOSApi.testing;

public class ApiTesting: IClassFixture<MockOsWebApplicationFactory<Program>>
{

    private readonly MockOsWebApplicationFactory<Program> _factory;

     public ApiTesting(MockOsWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }


    [Fact]

        public async Task ProcessCreationPostReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var body = """
            {
                "Pid": 15,
                "Image": "bin/ls",
                "Args": null,
                "Priority": 60
            }
            """;
            var requestBody = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/process", requestBody);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

        }
    [Theory]
    [InlineData("/api/process")]
    [InlineData("/api/process/3")]
    [InlineData("/api/process/all")]
  
    public async Task ProcessControllerEndpointsReturnsSuccess(string url)
    {
        var client = _factory.CreateClient();
          var response = await client.GetAsync(url);
           response.EnsureSuccessStatusCode();


    }
    

}