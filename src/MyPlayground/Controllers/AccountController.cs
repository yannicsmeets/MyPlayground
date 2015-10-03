using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MyPlayground.Exceptions;
using MyPlayground.Services;
using MyPlayground.ViewModels;
using System;
using System.Threading.Tasks;

namespace MyPlayground.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private readonly UserService service;

    public AccountController(
      UserService service)
    {
      this.service = service;
    }

    public IActionResult Index()
    {
      var users = service.AllUserViewModels();
      return View(users);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
      if (ModelState.IsValid)
      {
        var result = await service.SignIn(model);

        if (result.Succeeded)
        {
          return RedirectToLocal(returnUrl);
        }
        else if (result.IsLockedOut)
        {
          return View("Lockout");
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Ongeldige login poging");
        }
      }

      ViewBag.ReturnUrl = returnUrl;
      return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> LogOff()
    {
      await service.SignOut();
      return RedirectToAction(nameof(AccountController.Login));
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RegisterUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var result = await service.CreateUser(model);

        if (result.Succeeded)
        {
          return RedirectToAction(nameof(AccountController.Index));
        }
        else
        {
          AddErrors(result);
        }
      }

      return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id) //UserId
    {
      var model = await service.UpdateUserViewModel(id);
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        await service.UpdateUser(model);
        return RedirectToAction(nameof(AccountController.Index));
      }

      model.CurrentPassword = null;
      model.NewPassword = null;
      model.ConfirmPassword = null;
      return View(model);
    }

    [HttpPost]
    [HttpDelete]
    public async Task<JsonResult> Delete(string id)
    {
      var error = string.Empty;

      try
      {
        await service.DeleteUser(id);
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

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction(nameof(HomeController.Index), "Home");
      }
    }
  }
}
