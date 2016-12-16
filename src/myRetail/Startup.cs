using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myRetail.Models;
using Swashbuckle.Swagger.Model;

namespace myRetail
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddTransient<DataAccess>();
            services.AddMvc();
            
            //services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "myRetail Product API",
                    Description = "by @stevebargelt",
                    TermsOfService = "NA",
                    Contact = new Contact() { Name="stevebargelt", Email="steve@bargelt.com", Url="http://bargelt.com" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(opt => opt.MapRoute("default", "{controller}/{id?}"));
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
