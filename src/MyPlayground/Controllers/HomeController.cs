﻿using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace MyPlayground.Controllers
{
  public class HomeController : Controller
  {
    [Authorize]
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Error()
    {
      return View("~/Views/Shared/Error.cshtml");
    }
  }
}
