using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using mockOSApi.Data;
using System.Text;


namespace mockOSApi.testing;

public class CustomWebApplicationFactory<Program>
    : WebApplicationFactory<Program> where Program : class {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
            {
                var connectionString = "server=localhost;user=root;password=****;database=mockOSTest;";
        var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<OSDbContext>));

            services.Remove(dbContextDescriptor);

             services.AddDbContext<OSDbContext>(options =>
                {
                    options.UseMySql(connectionString,new MySqlServerVersion(new Version(11, 0, 2))); // Replace with your actual connection string
                });

                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    // Get the DbContext instance and apply migrations (if needed)
                    var dbContext = scope.ServiceProvider.GetRequiredService<OSDbContext>();
                    dbContext.Database.EnsureCreated();

    }
            });


    }
    }


public class ApiTesting: IClassFixture<CustomWebApplicationFactory<Program>>
{

    private readonly CustomWebApplicationFactory<Program> _factory;

     public ApiTesting(CustomWebApplicationFactory<Program> factory)
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