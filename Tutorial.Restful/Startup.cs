﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tutorial.Restful.Configurations;
using Tutorial.Restful.Data;
using Tutorial.Restful.Data.Repositories;
using Tutorial.Restful.Domain;
using Tutorial.Restful.Domain.Repositories;

namespace Tutorial.Restful
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            var constructorLogger = _loggerFactory.CreateLogger($"{nameof(Startup)}.Constructor");
            constructorLogger.LogInformation(1, "来自Startup.cs的构造函数的日志");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var methodLogger = _loggerFactory.CreateLogger($"Startup.ConfigureServices");
            methodLogger.LogInformation(2, "来自Startup.cs的ConfigureServices的日志");

            services.Configure<FirstConfig>(Configuration);

            services.AddDbContext<RestfulContext>(options =>
            {
                // Memory模式 名字随便取
                options.UseInMemoryDatabase("RestfulDatabase");
                options.UseLoggerFactory(_loggerFactory);
            });

            services.AddMvc(options => { options.Filters.Add<DefaultNameFilter>(); });

            services.AddAutoMapper();

            AddMyProject(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IOptions<FirstConfig> firstConfig,
            IOptionsSnapshot<FirstConfig.Key3Options> key3Options,
            ILogger<Startup> logger)
        {
            logger.LogDebug($"Start Up Configure key1: {firstConfig.Value.Key1}");
            logger.LogInformation($"Start Up Configure key1: {firstConfig.Value.Key1}");


            // TODO: 存在问题
            Console.WriteLine($"Start up configure key3: {key3Options.Value.ChildKey1}");

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync(
                    $"My status page, status code is: {context.HttpContext.Response.StatusCode}");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An error occurred");
                    });
                });
            }

            app.UseStaticFiles();

            app.UseCors();

            // 开启Swagger UI 中间件和 Swagger generator

            app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, setting =>
                {
                    setting.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
                    setting.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Tutorial Restful API";
                        document.Info.Description = "Restful API 的示例";
                        document.Info.TermsOfService = "None";
                    };
                });

            app.UseMvc();
        }

        private static void AddMyProject(IServiceCollection services)
        {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
