using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TTechTash.Domain.DTOs
{
    public class UserDTO
    {
        public class Register
        {
            [Required(ErrorMessage = "Username Is Required")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Email Is Required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }
            [Required(ErrorMessage = "PhoneNumber Is Required")]
            public string PhoneNumber { get; set; }
            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Confirmation Password is required.")]
            [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
            public string ConfirmPassword { get; set; }
            //1- Admin 2- User 3-Tester
            public byte UserType { get; set; }
        }
        public class Get
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
        public class Login
        {
            public string UserNameOrEmail { get; set; }
            public string Password { get; set; }
            //1- Admin 2- User 3-Tester
            public byte UserType { get; set; }
        }
        public class LoginResult : Get
        {
            public string Token { get; set; }
        }
    }
}
