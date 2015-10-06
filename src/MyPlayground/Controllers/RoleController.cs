using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using MyPlayground.Exceptions;
using MyPlayground.Services;
using MyPlayground.ViewModels;
using System;
using System.Threading.Tasks;

namespace MyPlayground.Controllers
{
  [Authorize]
  public class RoleController : Controller
  {
    private readonly RoleService service;

    public RoleController(
      RoleService service)
    {
      this.service = service;
    }

    public IActionResult Index()
    {
      var roles = service.AllRoleViewModels();
      return View(roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRoleViewModel model)
    {
      if (ModelState.IsValid)
      {
        await service.CreateRole(model);
        return RedirectToAction(nameof(RoleController.Index));
      }

      return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id) //RoleId
    {
      var model = await service.UpdateRoleViewModel(id);
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateRoleViewModel model)
    {
      if (ModelState.IsValid)
      {
        await service.UpdateRole(model);
        return RedirectToAction(nameof(RoleController.Index));
      }

      return View(model);
    }

    [HttpPost]
    [HttpDelete]
    public async Task<JsonResult> Delete(string id) //RoleId
    {
      var error = string.Empty;

      try
      {
        await service.DeleteRole(id);
      }
      catch(MyPlaygroundException e)
      {
        error = e.Message;
      }
      catch(Exception e)
      {
        error = e.Message;
      }

      return Json(new
      {
        error = error
      });
    }
  }
}
