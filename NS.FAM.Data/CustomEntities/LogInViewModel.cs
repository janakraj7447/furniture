using System.ComponentModel.DataAnnotations;

namespace NS.FAM.Data.CustomEntities
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email ID is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your correct Password")]
        public string Password { get; set; }
    }
}
