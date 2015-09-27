﻿using System.Collections.Generic;

namespace MyPlayground.ViewModels
{
  public class UserViewModel
  {
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public IEnumerable<string> Rights { get; set; }
  }
}
