using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MyPlayground.Exceptions;
using MyPlayground.Services;
using MyPlayground.ViewModels;
using System;
using System.Threading.Tasks;

namespace MyPlayground.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserService service;

    public AccountController(
      UserService service)
    {
      this.service = service;
    }

    public IActionResult Index()

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
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

    public async Task<IActionResult> LogOff()
    {
      await service.SignOut();
      return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]

    [HttpPost]

        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        {
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
