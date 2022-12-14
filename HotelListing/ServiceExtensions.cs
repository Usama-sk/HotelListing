
using AspNetCoreRateLimit;
using DataModels.Models;
using DataServiceLayer.DataService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Collections.Generic;
using System.Text;

namespace HotelListing
{

    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>(q => q.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSetting = Configuration.GetSection("JWT");
            var key = "This is my supper secret key for jwt";

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

                };
            });



        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    Log.Error($"Something Went Wrong in the {contextFeature.Error}");

                    await context.Response.WriteAsync(new Error
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error, Please Try Again Later"
                    }.ToString());
                }
            });

            });
        }
        public static void ConfigureVersioning (this IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {
                option.ReportApiVersions = true;
            option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1,0);
                option.ApiVersionReader = new HeaderApiVersionReader("api-version");
                });
        }
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddHttpCacheHeaders(
                (expirationOpt)=>
                {
                    expirationOpt.MaxAge = 120;
                    expirationOpt.CacheLocation = CacheLocation.Private;
                },
                (validationOpt)=>
                {
                    validationOpt.MustRevalidate = true;    
                });
        }
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint="*",
                    Limit=1,
                    Period="5s"
                }
            };
            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

        }
    }

}
