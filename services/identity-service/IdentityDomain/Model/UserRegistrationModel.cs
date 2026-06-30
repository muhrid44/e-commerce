using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Model
{
    public class UserRegistrationModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
