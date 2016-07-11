using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AkuznetsovReddit.Web.Models
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Confirm password please")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DisplayName("Username")]
        public string UserName { get; set; }
    }
}
