using mockOSApi.Data;
using mockOSApi.Repository;
using Microsoft.EntityFrameworkCore;
using mockOSApi.Services;
using mockOSApi.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase");

builder.Services.AddDbContext<OSDbContext>(
           dbContextOptions => dbContextOptions
               .UseMySql(connectionString, new MySqlServerVersion(new Version(11, 0, 2))) //should handle this hardcoding for server version
                                                                                          // The following three options help with debugging, but should
                                                                                          // be changed or removed for production.
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors()
       );

builder.Services.AddScoped(typeof(IProcessRepository), typeof(ProcessRepositoryDb));
builder.Services.AddScoped(typeof(IProcessHandler), typeof(ProcessHandler));
builder.Services.AddScoped(typeof(IMapper), typeof(ProcessMapper));

var app = builder.Build();



app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapControllers();

app.Run();
