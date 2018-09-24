using System;
using AuthorizationServer.Data;
using AuthorizationServer.Domain;
using AuthorizationServer.Interfaces;
using AuthorizationServer.Repositories;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer(options =>
        {
            options.UserInteraction.ConsentUrl = "~/Consent";
        })
        .AddInMemoryApiResources(Config.GetApiResources())
        .AddInMemoryIdentityResources(Config.GetIdentityResources());

            builder.AddClientStore<ClientStore>();
            builder.AddProfileService<ProfileService>();

            // TODO : replace by real credential for production
            builder.AddDeveloperSigningCredential();

            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddTransient<IClientRepository, InMemoryClientRepository>();
            services.AddSingleton<ICorsPolicyService, CorsPolicyService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder =>
                corsBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

                options.AddPolicy("AllowLocals", corsBuilder =>
                corsBuilder.WithOrigins("https://localhost:50001", "http://localhost:5001", "http://localhost:4200"));
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ServerDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ServerDbContext")));
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors("AllowAll");
            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
