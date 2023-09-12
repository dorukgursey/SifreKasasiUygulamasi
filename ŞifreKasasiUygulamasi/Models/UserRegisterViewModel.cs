using System.ComponentModel.DataAnnotations;

namespace IdentityWebApplication.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter Your Name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Surname!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please Enter Your Username!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email!")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password Again!")]
        [Compare("Password",ErrorMessage = "Passwords Don't Match!")]
        public string ConfirmPassword { get; set; }
    }
}
