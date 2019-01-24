using AutoMapper;
using Grapefruit.WebApi.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            #region Dependency Injection Services

            Assembly assembly = Assembly.Load("Grapefruit.Application");
            foreach (var implement in assembly.GetTypes())
            {
                Type[] interfaceType = implement.GetInterfaces();
                foreach (var service in interfaceType)
                {
                    services.AddTransient(service, implement);
                }
            }

            #endregion

            #region Configure Jwt Authentication

            //Use Jwt bearer authentication
            //
            string issuer = Configuration["Jwt:Issuer"];
            string audience = Configuration["Jwt:Audience"];
            string expire = Configuration["Jwt:ExpireMinutes"];
            TimeSpan expiration = TimeSpan.FromMinutes(Convert.ToDouble(expire));
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]));

            services.AddAuthorization(options =>
            {
                //1、Definition authorization policy
                options.AddPolicy("Permission",
                   policy => policy.Requirements.Add(new PolicyRequirement()));
            }).AddAuthentication(s =>
            {
                //2、Authentication
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(s =>
            {
                //3、Use Jwt bearer 
                s.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = key,
                    ClockSkew = expiration,
                    ValidateLifetime = true
                };
                s.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            //DI handler process function
            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();

            #endregion

            #region MVC

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #endregion

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
            }).AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVVV";//api group name
                option.AssumeDefaultVersionWhenUnspecified = true;//whether provide a service API version
            });

            #endregion

            #region Configure Swagger

            services.AddSwaggerGen(s =>
            {
                //Generate api description doc
                //
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    s.SwaggerDoc(description.GroupName, new Info
                    {
                        Contact = new Contact
                        {
                            Name = "Danvic Wang",
                            Email = "danvic96@hotmail.com",
                            Url = "https://yuiter.com"
                        },
                        Description = "A front-background project build by ASP.NET Core 2.1 and Vue",
                        Title = "Grapefruit.VuCore",
                        Version = description.ApiVersion.ToString(),
                        License = new License
                        {
                            Name = "MIT",
                            Url = "https://mit-license.org/"
                        }
                    });
                }

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

            #region Others

            services.AddAutoMapper();

            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = Configuration["Redis:ConnectionString"];
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
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

            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add((document, request) =>
                {
                    document.Paths = document.Paths.ToDictionary(p => p.Key.ToLowerInvariant(), p => p.Value);
                });
            });

            app.UseSwaggerUI(s =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    s.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        $"Grapefruit.VuCore API {description.GroupName.ToUpperInvariant()}");
                }
            });

            #endregion
        }
    }
}
