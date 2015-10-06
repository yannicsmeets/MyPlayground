using System.ComponentModel.DataAnnotations;

namespace MyPlayground.ViewModels
{
  public class CreateRoleViewModel
  {
    [Required]
    [Display(Name = "Rolnaam")]
    public string Name { get; set; }
  }
}
