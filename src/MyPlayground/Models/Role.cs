using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MyPlayground.Models
{
  public class Role : IdentityRole
  {
    public virtual ICollection<Claim> CustomClaims { get; } = new List<Claim>();
  }
}
