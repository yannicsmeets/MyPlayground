using Microsoft.AspNet.Builder;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Identity;
using MyPlayground.Models;
using MyPlayground.Configuration;
using Microsoft.Framework.OptionsModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MyPlayground.Context
{
  public static class DbInitializer
  {
    public static async Task Initialize(this IApplicationBuilder app)
    {
      var context = app.ApplicationServices.GetService<MyPlaygroundDbContext>();
      var userManager = app.ApplicationServices.GetService<UserManager<User>>();
      var roleManager = app.ApplicationServices.GetService<RoleManager<Role>>();
      var defaultAdminSettings = app.ApplicationServices.GetService<IOptions<DefaultAdminSettings>>()?.Options;

      // Create all roles
      var roleNames = new List<string> { Constants.AdminRoleName };

      foreach(var roleName in roleNames)
      {
        if (await roleManager.FindByNameAsync(roleName) == null)
        {
          var role = new Role { Name = roleName };
          await roleManager.CreateAsync(role);
        }
      }

      // Create Admin User if no admin exists
      var adminRole = await roleManager.FindByNameAsync(Constants.AdminRoleName);
      if (!adminRole.Users.Any())
      {
        var defaultAdmin = new User
        {
          UserName = defaultAdminSettings.UserName,
          Email = defaultAdminSettings.Email,
          EmailConfirmed = true,
          PhoneNumber = defaultAdminSettings.PhoneNumber,
          PhoneNumberConfirmed = true,
          Created = DateTimeOffset.Now
        };

        await userManager.CreateAsync(defaultAdmin);
        await userManager.AddPasswordAsync(defaultAdmin, defaultAdminSettings.Password);
        await userManager.AddToRoleAsync(defaultAdmin, Constants.AdminRoleName);
      }
    }
  }
}
