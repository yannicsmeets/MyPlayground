﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using MyPlayground.Models;
using MyPlayground.Services;

namespace MyPlayground
{
  public class Startup
  {
    public IConfigurationRoot Configuration { get; set; }

    public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
    {
      // Setup configuration sources.

      var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
          .AddJsonFile("config.json")
          .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

      if (env.IsDevelopment())
      {
        // This reads the configuration keys from the secret store.
        // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
        builder.AddUserSecrets();
      }
      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add Entity Framework services to the services container.
      services.AddEntityFramework()
          .AddSqlServer()
          .AddDbContext<MyPlaygroundDbContext>(options =>
              options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

      // Add Identity services to the services container.
      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      // Add MVC services to the services container.
      services.AddMvc();
    }

    // Configure is called after ConfigureServices is called.
    public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.MinimumLevel = LogLevel.Information;
      loggerFactory.AddConsole();
      loggerFactory.AddDebug();

      // Configure the HTTP request pipeline.

      // Add the following to the request pipeline only in development environment.
      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseErrorPage();
        app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
      }
      else
      {
        // Add Error handling middleware which catches all application specific errors and
        // sends the request to the following path or controller action.
        app.UseErrorHandler("/Home/Error");
      }

      // Add static files to the request pipeline.
      app.UseStaticFiles();

      // Add cookie-based authentication to the request pipeline.
      app.UseIdentity();

      // Add MVC to the request pipeline.
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });

      await app.Initialize();
    }
  }
}
