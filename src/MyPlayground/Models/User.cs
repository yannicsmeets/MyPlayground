using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace MyPlayground.Models
{
  public class User : IdentityUser
  {
    public DateTimeOffset Created { get; set; }

    public DateTimeOffset? Deleted { get; set; }
  }
}
