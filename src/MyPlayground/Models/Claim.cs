using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPlayground.Models
{
  public class Claim
  {
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string ClaimType { get; set; }

    public string ClaimValue { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
  }
}
