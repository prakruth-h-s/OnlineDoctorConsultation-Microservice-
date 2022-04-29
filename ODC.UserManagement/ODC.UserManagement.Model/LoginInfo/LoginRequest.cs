using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODC.UserManagement.Model
{
    public class LoginRequest
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

    }
}
