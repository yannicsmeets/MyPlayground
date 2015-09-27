using Microsoft.AspNet.Identity;
using MyPlayground.Context;
using MyPlayground.Models;
using System;
using System.Linq;

namespace MyPlayground.Services
{
  public class RoleService : BaseService
  {
    RoleManager<Role> roleManager { get { return GetService<RoleManager<Role>>(); } }

    public RoleService(MyPlaygroundDbContext db, IServiceProvider sp)
      : base(db, sp)
    {

    }

    public IQueryable<Role> AllRoles()
    {
      return roleManager.Roles;
    }
  }
}
