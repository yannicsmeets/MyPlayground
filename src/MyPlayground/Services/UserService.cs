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

    public UserService(
      MyPlaygroundDbContext db,
      IServiceProvider sp)
      : base(db, sp)
    { }

    public async Task<SignInResult> SignIn(LoginViewModel model)
    {
      return await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
    }

    public async Task SignOut()
    {
      await signInManager.SignOutAsync();
    }

  }
}
