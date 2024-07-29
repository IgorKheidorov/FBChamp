using System.ComponentModel.DataAnnotations;

namespace FBChamp.Web.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    [Display(Prompt = "Enter email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Prompt = "Enter password")]
    public string Password { get; set; }
}