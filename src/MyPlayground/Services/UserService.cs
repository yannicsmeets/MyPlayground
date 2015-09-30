using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using MyPlayground.Configuration;
using MyPlayground.Context;
using MyPlayground.Exceptions;
using MyPlayground.Models;
using MyPlayground.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlayground.Services
{
  public class UserService : BaseService
  {
    UserManager<User> userManager { get { return GetService<UserManager<User>>(); } }
    SignInManager<User> signInManager { get { return GetService<SignInManager<User>>(); } }

    RoleService roleService { get { return GetService<RoleService>(); } }

    public UserService(
      MyPlaygroundDbContext db,
      IServiceProvider sp)
      : base(db, sp)
    { }

    public IQueryable<User> AllUsers()
    {
      return userManager.Users;
    }

    public IQueryable<User> AllActiveUsers()
    {
      return AllUsers().Where(u => u.Deleted == null);
    }

    public async Task<SignInResult> SignIn(LoginViewModel model)
    {
      return await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
    }

    public async Task SignOut()
    {
      await signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> CreateUser(RegisterUserViewModel model)
    {
      var user = new User
      {
        UserName = model.UserName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber
      };

      return await userManager.CreateAsync(user, model.Password);
    }

    public async Task<UpdateUserViewModel> UpdateUserViewModel(string userId)
    {
      var user = await userManager.FindByIdAsync(userId);
      return new UpdateUserViewModel
      {
        Id = userId,
        UserName = user.UserName,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
      };
    }

    public IEnumerable<UserViewModel> AllUserViewModels()
    {
      var users = AllActiveUsers().Include(u => u.Roles).ToList();
      var roles = roleService.AllRoles().ToList();

      return users.Select(u => new UserViewModel
      {
        Id = u.Id,
        UserName = u.UserName,
        Email = u.Email,
        PhoneNumber = u.PhoneNumber,
        Roles = roles.Where(r => u.Roles.Select(ur => ur.RoleId)
                                  .Any(rid => rid == r.Id))
                     .Select(r => r.Name)
      });
    }

    public async Task UpdateUser(UpdateUserViewModel model)
    {
      var user = await userManager.FindByIdAsync(model.Id);
      user.UserName = model.UserName;
      user.Email = model.Email;
      user.PhoneNumber = model.PhoneNumber;

      await userManager.UpdateAsync(user);

      if (model.NewPassword != null)
      {
        await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
      }
    }

    public async Task DeleteUser(string userId)
    {
      var user = await userManager.FindByIdAsync(userId);
      if (await userManager.IsInRoleAsync(user, Constants.AdminRoleName))
        throw new MyPlaygroundException("Deze gebruiker is een admin en kan niet worden verwijderd");

      user.Deleted = DateTimeOffset.Now;
      await userManager.UpdateAsync(user);
    }
  }
}
