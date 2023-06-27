using System.ComponentModel.DataAnnotations;

namespace ClientsWebApp.Blazor.Pages.Identity.Models
{
    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
