using StaffManagment.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [ValidEmailDomain(allowedDomain: "avcol.school.nz",
        ErrorMessage = "Email domain must be avcol.school.nz")]
        //[Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
