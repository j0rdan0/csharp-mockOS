using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using mockOSApi.Data;
using System.Text;


namespace mockOSApi.testing;

public class MockOsWebApplicationFactory<Program>
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