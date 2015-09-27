using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MyPlayground.Exceptions;
using MyPlayground.Services;
using MyPlayground.ViewModels;
using System;
using System.Threading.Tasks;

namespace MyPlayground.Controllers
{

    public AccountController(

    public IActionResult Index()

    [HttpGet]

    [HttpPost]

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

      ViewBag.ReturnUrl = returnUrl;

    public async Task<IActionResult> LogOff()

    [HttpGet]

    [HttpPost]

        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        {
        }






    }
}
