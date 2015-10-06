using Microsoft.AspNet.Identity;
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
  public class RoleService : BaseService
  {
    RoleManager<Role> roleManager { get { return GetService<RoleManager<Role>>(); } }

    public RoleService(MyPlaygroundDbContext db, IServiceProvider sp)
      : base(db, sp)
    {

    }

    public async Task<Role> GetRole(string roleId)
    {
      var role = await roleManager.FindByIdAsync(roleId);
      if (role == null)
        throw new MyPlaygroundException("De rol kon niet worden gevonden");

      return role;
    }

    public IQueryable<Role> AllRoles()
    {
      return roleManager.Roles;
    }

    public IEnumerable<RoleViewModel> AllRoleViewModels()
    {
      return AllRoles().Select(r => new RoleViewModel
      {
        Id = r.Id,
        Name = r.Name,
        Rights = r.CustomClaims.Where(c => c.ClaimType == Constants.RightsClaimType)
                  .Select(c => c.ClaimValue)
      });
    }

    public async Task CreateRole(CreateRoleViewModel model)
    {
      var role = new Role
      {
        Name = model.Name
      };

      await roleManager.CreateAsync(role);
    }

    public async Task<UpdateRoleViewModel> UpdateRoleViewModel(string roleId)
    {
      var role = await GetRole(roleId);
      return new UpdateRoleViewModel
      {
        Id = role.Id,
        Name = role.Name
      };
    }

    public async Task UpdateRole(UpdateRoleViewModel model)
    {
      var role = await GetRole(model.Id);
      if (!CanBeAltered(role))
        throw new MyPlaygroundException("De administrator rol kan niet worden veranderd");

      role.Name = model.Name;
      await roleManager.UpdateAsync(role);
    }

    public async Task DeleteRole(string roleId)
    {
      var role = await roleManager.FindByIdAsync(roleId);
      if (!CanBeAltered(role))
        throw new MyPlaygroundException("De administrator rol kan niet worden verwijderd");

      await roleManager.DeleteAsync(role);
    }

    private bool CanBeAltered(Role role)
    {
      return !role.Name.Equals(Constants.AdminRoleName);
    }
  }
}
