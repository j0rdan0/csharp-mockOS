public static class Startup
{
    
    public static void StartBoot(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
{   
    var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("Startup");
     
    lifetime.ApplicationStarted.Register(() =>
    {
      logger.LogInformation("TBD for boot process");
    });
}

}