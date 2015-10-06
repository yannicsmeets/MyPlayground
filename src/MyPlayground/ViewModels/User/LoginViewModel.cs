using System.ComponentModel.DataAnnotations;

namespace MyPlayground.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name = "Gebruikersnaam")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord")]
    public string Password { get; set; }

    [Display(Name = "Onthoud mij")]
    public bool RememberMe { get; set; }
  }
}
