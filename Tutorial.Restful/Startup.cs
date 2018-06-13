using System;
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
using Tutorial.Restful.Filters;

namespace Tutorial.Restful
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add<DefaultNameFilter>(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

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

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
