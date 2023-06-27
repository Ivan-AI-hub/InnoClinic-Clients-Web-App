using System.ComponentModel.DataAnnotations;

namespace ClientsWebApp.Blazor.Pages.Identity.Models
{
    public class RegisterData
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }
    }
}
