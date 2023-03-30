using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You must type First Name user.")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "You must type Last Name user.")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "You must type Email user")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "You must type User Name.")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "You must type a password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        [Required(ErrorMessage = "Should type a password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You must type phone number.")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
