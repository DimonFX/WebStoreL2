using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;
namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
               .Build()
               .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((host, log) =>
            {
                //log.ClearProviders();
                //log.AddProvider(new ConsoleLoggerProvider());
                //log.AddConsole(opt=> opt.IncludeScopes=true);
                //log.AddEventLog(opt => opt.LogName = "WebStore");//Провайдер, который занимается сохранением событий в системынй журнал ОС
                //log.AddFilter("System", LogLevel.Warning);
                //log.AddFilter((category, level) =>
                //{
                //    if (category.StartsWith("Microsoft")) return level >= LogLevel.Warning;
                //    return true;
                //});
            })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                })
               .UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration)
                   .MinimumLevel.Debug()
                   .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
                   .Enrich.FromLogContext()
                   .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
                   .WriteTo.RollingFile($@".\Logs\WebStore[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log")
                   .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(",", true), $@".\Logs\WebStore[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log.json")
                   .WriteTo.Seq("http://localhost:5341/"));
    }
}
