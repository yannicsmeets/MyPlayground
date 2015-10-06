using System.ComponentModel.DataAnnotations;

namespace MyPlayground.ViewModels
{
  public class UpdateRoleViewModel
  {
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }
  }
}
