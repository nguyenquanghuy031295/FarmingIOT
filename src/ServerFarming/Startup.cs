using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FarmingDatabase.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServerFarming.Core.Services;
using ServerFarming.Core.Services.Implement;
using ServerFarming.Core.Repositories;
using ServerFarming.Core.Repositories.Implement;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ServerFarming
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

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("FarmingDatabase");
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            // Add Cross-Origin Requests
            services.AddCors();

            services.AddDbContext<FarmingDbContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("ServerFarming")));

            // Add HangFire
            // services.AddHangfire(x => x.UseSqlServerStorage(connection));

            // Add Identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<IdentityUser<long>, IdentityRole<long>>()
                .AddEntityFrameworkStores<FarmingDbContext, long>();

            //configure identity options
            services.Configure<IdentityOptions>(options =>
            {
                // Lockout settings: disable lock out
                //options.Lockout.MaxFailedAccessAttempts = int.MaxValue;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "";
                options.Cookies.ApplicationCookie.LogoutPath = "/Logout";
                options.Cookies.ApplicationCookie.CookieHttpOnly = true;

                options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.FromResult(0);
                    }
                };

                // User settings
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;

                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
            });

            //Add Services
            services.AddTransient<IFarmService, FarmService>();
            services.AddTransient<IPlantService, PlantService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IDeviceService, DeviceService>();

            //Add Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFarmRepository, FarmRepository>();
            services.AddTransient<IPlantRepository, PlantRepository>();
            services.AddTransient<ISensorRepository, SensorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // Use HangFire
            // app.UseHangfireServer();

            // User ASP Identity
            app.UseIdentity();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:36539", "http://localhost:5050")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseMvc();

            //RecurringJob.......
        }
    }
}
