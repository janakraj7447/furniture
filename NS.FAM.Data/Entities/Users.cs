using System;
using System.Collections.Generic;
using NS.FAM.Data.CustomEntities;

namespace NS.FAM.Data.Entities
{
    public partial class Users
    {
        public Users() { }
        public Users(UserViewModel user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            ProfilePic = user.ProfilePic;
            Age = user.Age;
            Gender = user.Gender;
            Address = user.Address;
            PinCode = user.PinCode;
            RoleId = Convert.ToInt64(Common.Role.User);
            PhoneNo = user.PhoneNo;
            Email = user.Email;
            Password = user.Password;
            CountryId = user.CountryId;
            StateId = user.StateId;
            CityId = user.CityId;
            CreatedDate = DateTime.Now;
            IsActive = true;
            IsDeleted = false;

        }

        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long RoleId { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public string PinCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string PhoneNo { get; set; } = null!;
        public string? Gender { get; set; }
        public string Email { get; set; } = null!;
        public string ProfilePic { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    }
}
