using mockOSApi.Data;
using mockOSApi.Repository;
using Microsoft.EntityFrameworkCore;
using mockOSApi.Services;
using mockOSApi.DTO;
using mockOSApi.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;


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

// should implement real JWT token generation handler
builder.Services.AddAuthorizationBuilder()
  .AddPolicy("admin_policy", policy =>
        policy
            .RequireRole("admin")
            .RequireClaim("scope", "api"));


builder.Services.AddHttpLogging(o => { });
builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddScoped(typeof(IProcessRepository), typeof(ProcessRepositoryDb));
builder.Services.AddScoped(typeof(IProcessService), typeof(ProcessService));
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