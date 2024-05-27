using System.ComponentModel.DataAnnotations;

namespace NS.FAM.Data.CustomEntities
{
    public class UserViewModel
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        public long RoleId { get; set; }

        [Range(1, 120, ErrorMessage = "Age must be between 1-120 in years.")] 
        public int Age { get; set; }

        public string Address { get; set; }
        [Required(ErrorMessage = "Please select a gender")]
        public string Gender { get; set; }


        [RegularExpression(@"^([0-9]{6})$", ErrorMessage = "Please Enter Valid PinCode.")]
        public string PinCode { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        public string PhoneNo { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}", ErrorMessage = "Incorrect Email Format")]
      //  [Remote(action: "VerifyEmail", controller: "User")]
        public string Email { get; set; }
        [Required]
        public string? ProfilePic { get; set; }

        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        [Required]
        public int? CountryId { get; set; }
        [Required]
        public int? StateId { get; set; }
        [Required]
        public int? CityId { get; set; }

    }
}
