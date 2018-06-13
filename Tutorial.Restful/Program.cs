using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tutorial.Restful.Configurations;
using Tutorial.Restful.Data;

namespace Tutorial.Restful
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            DealWithConfiguration();

            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var restfulContext = services.GetRequiredService<RestfulContext>();
                    RestfulContextSeed.SeedAsync(restfulContext, loggerFactory).Wait();
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "增加种子数据的时候发生异常");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void DealWithConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations\\FirstConfig.json")
                .AddJsonFile("Configurations\\SecondConfig.json");

            var firstConfig = new FirstConfig();


            Configuration = configBuilder.Build();
            Configuration.Bind(firstConfig);

            Console.WriteLine($"FirstConfig class Key1: {firstConfig.Key1}");
            Console.WriteLine($"FirstConfig class ChildKey1: {firstConfig.Key3.ChildKey1}");

            foreach (var item in Configuration.AsEnumerable())
            {
                System.Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }

            System.Console.WriteLine($"Key1: {Configuration["key1"]}");
            System.Console.WriteLine($"Key2: {Configuration["key2"]}");
            System.Console.WriteLine($"childkey1: {Configuration["key3:childkey1"]}");

            var key3Section = Configuration.GetSection("key3");
            Console.WriteLine($"key3:childkey1: {key3Section["childkey1"]}");
        }
    }
}
