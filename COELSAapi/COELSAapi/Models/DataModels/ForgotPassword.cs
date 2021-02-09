using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COELSAapi.Models.DataModels
{
    public class ForgotPassword
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}