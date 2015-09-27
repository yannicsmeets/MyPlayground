using System.ComponentModel.DataAnnotations;

namespace MyPlayground.ViewModels
{
  public class RegisterUserViewModel
  {
    [Required]
    [Display(Name = "Gebruikersnaam")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen")]
    [Display(Name = "Herhaal wachtwoord")]
    public string ConfirmPassword { get; set; }

    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Phone]
    [Display(Name = "Telefoonnummer")]
    public string PhoneNumber { get; set; }
  }
}
