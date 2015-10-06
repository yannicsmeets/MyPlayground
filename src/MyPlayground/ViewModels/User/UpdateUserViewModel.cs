using System.ComponentModel.DataAnnotations;

namespace MyPlayground.ViewModels
{
  public class UpdateUserViewModel
  {
    [Required]
    public string Id { get; set; }

    [Required]
    [Display(Name = "Gebruikersnaam")]
    public string UserName { get; set; }

    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Phone]
    [Display(Name = "Telefoonnummer")]
    public string PhoneNumber { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Huidig wachtwoord")]
    public string CurrentPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Nieuw wachtwoord")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Herhaal wachtwoord")]
    [Compare("NewPassword", ErrorMessage = "De wachtwoorden komen niet overeen")]
    public string ConfirmPassword { get; set; }
  }
}
