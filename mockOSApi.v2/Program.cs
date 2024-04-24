using mockOSApi.Data;
using mockOSApi.Repository;
using Microsoft.EntityFrameworkCore;
using mockOSApi.Services;
using mockOSApi.DTO;
using mockOSApi.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.Extensions.Azure;
using Azure.Identity;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase");
var vaultUri = builder.Configuration["VaultURI"];

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddDbContext<OSDbContext>(
           dbContextOptions => dbContextOptions
               .UseMySql(connectionString, new MySqlServerVersion(new Version(11, 0, 2))) //should handle this hardcoding for server version
                                                                                          // The following three options help with debugging, but should
                                                                                          // be changed or removed for production.
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors()
       );

builder.Services.AddHttpLogging(o => { });

builder.Services.AddScoped(typeof(IProcessRepository), typeof(ProcessRepositoryDb));
builder.Services.AddScoped(typeof(IProcessService), typeof(ProcessService));
builder.Services.AddScoped(typeof(IAuthentication), typeof(AuthenticationService));
builder.Services.AddScoped(typeof(IUserRepositoryKv), typeof(UserRepository));
builder.Services.AddScoped(typeof(IMockProcessBuilder), typeof(MockProcessBuilder));

builder.Services.AddAzureClients(builder =>
{

#pragma warning disable CS8604 // Possible null reference argument.
    builder.AddSecretClient(new Uri(vaultUri));
#pragma warning restore CS8604 // Possible null reference argument.
    builder.UseCredential(new EnvironmentCredential());
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2",
        Title = "mockOS API",
        Description = "An ASP.NET Core Web API for simulating the basic functions of an operating system",

    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}); //added Swagger


builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<MockProcess, MockProcessDto>();
    cfg.CreateMap<MockProcessCreationDto, MockProcess>();
    cfg.CreateMap<MockProcessDto, MockProcess>();
    cfg.CreateMap<MockProcessCreationDto, MockProcessDto>();

    cfg.CreateMap<User, UserDTO>();
    cfg.CreateMap<User, UserCreationDTO>();
    cfg.CreateMap<UserCreationDTO, User>();
    cfg.CreateMap<UserDTO, User>();

});
var app = builder.Build();

// app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseResponseCaching();
app.Run();

public partial class Program { }