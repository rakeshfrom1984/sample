using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cargo.API.Models.ViewModels
{
    public class Login
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}