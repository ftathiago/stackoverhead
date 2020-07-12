// <copyright file="Startup.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>

using System.IO.Compression;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using StackOverHead.CrossCutting.Extensions;
using StackOverHead.Web.Extensions;
using StackOverHead.Web.Models;

namespace StackOverHead.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.BuildConnectionString();
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            if (Env.EnvironmentName == "Integration")
            {
                services.AddMemoryDatabase();
            }
            else
            {
                services.AddPostgres(assemblyName, connectionString);
            }

            var startup = typeof(Startup);
            services
                .AddDependencies(startup, Configuration)
                .AddRouting(options =>
                {
                    options.LowercaseUrls = true;
                    options.ConstraintMap.Add("SearchQuestionRequest", typeof(SearchQuestionRequest));
                })
                .AddWebDependencies()
                .AddSwaggerConfig()
                .AddJWTConfig(Configuration)
                .AddCors()
                .Configure<GzipCompressionProviderOptions>(
                    options => options.Level = CompressionLevel.Optimal)
                .AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>())
                .AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(options =>
                options
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin())
                .UseProblemDetailsExceptionHandler(loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetExecutingAssembly().GetName().Name))
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action=Index}/{id?}");
                })
                .UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        // spa.UseAngularCliServer(npmScript: "start");
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                    }
                });
        }
    }
}
