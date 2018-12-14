using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Grapefruit.WebApi
{
    public class Startup
    {
        //Cors policy name
        private const string _defaultCorsPolicyName = "Grapefruit.Cors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Configure Cors

            services.AddCors(options => options.AddPolicy(_defaultCorsPolicyName,
                builder => builder.WithOrigins(
                        Configuration["Application:CorsOrigins"]
                        .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray()
                    )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));

            #endregion

            #region Configure Swagger

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Contact = new Contact
                    {
                        Name = "Danvic Wang",
                        Email = "danvic96@hotmail.com",
                        Url = "https://yuiter.com"
                    },
                    Description = "A front-background project build by ASP.NET Core 2.1 and Vue",
                    Title = "Grapefruit.VuCore",
                    Version = "v1"
                });

                //Add comments description
                //
                var basePath = Path.GetDirectoryName(AppContext.BaseDirectory);//get application located directory
                var xmlPath = Path.Combine(basePath, "Grapefruit.WebApi.xml");
                s.IncludeXmlComments(xmlPath, true);
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Enable Cors
            app.UseCors(_defaultCorsPolicyName);

            app.UseHttpsRedirection();
            app.UseMvc();

            #region Enable Swagger

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Grapefruit.VuCore API V1.0");
            });

            #endregion
        }
    }
}
