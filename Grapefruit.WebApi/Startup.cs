using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
            #region Configure Jwt Authentication

            services.AddAuthentication(s =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(s =>
            {
                s.TokenValidationParameters = new TokenValidationParameters
                {
                    //NameClaimType = JwtClaimTypes.
                    ValidIssuer = "yuiter.com",
                    ValidAudience = "yuiter.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]))
                };
            });

            #endregion

            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper();

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

            #region Configure API Version

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;//return versions in a response header
                o.DefaultApiVersion = new ApiVersion(1, 0);//default version select 
                o.AssumeDefaultVersionWhenUnspecified = true;//if not specifying an api version,show the default version
            });

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

                //Show the api version in url address
                s.DocInclusionPredicate((version, apiDescription) =>
                {
                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", version));

                    apiDescription.RelativePath = string.Join("/", values);

                    return true;
                });

                //Add comments description
                //
                var basePath = Path.GetDirectoryName(AppContext.BaseDirectory);//get application located directory
                var apiPath = Path.Combine(basePath, "Grapefruit.WebApi.xml");
                var dtoPath = Path.Combine(basePath, "Grapefruit.Application.xml");
                s.IncludeXmlComments(apiPath, true);
                s.IncludeXmlComments(dtoPath, true);

                //Add Jwt Authorize to http header
                s.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//Jwt default param name
                    In = "header",//Jwt store address
                    Type = "apiKey"//Security scheme type
                });
                //Add authentication type
                s.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            //Enable Cors
            app.UseCors(_defaultCorsPolicyName);

            //Enable Authentication
            app.UseAuthentication();

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
