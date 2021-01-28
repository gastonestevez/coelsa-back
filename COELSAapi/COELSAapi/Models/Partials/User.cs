using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COELSAapi.Models
{

    public partial class User
    {
        public User()
        {
            this.Role = (int)UtilCodes.Role.NewsSetter;
            this.Updated_At = DateTime.Now;
        }
        public User(string name, string email, string password, int role, DateTime created_at, string company) : this()
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.Created_At = created_at;
            this.Company = company;
        }

        public string RoleName => UtilCodes.GetRoleName(this.Role);
        public override string ToString()
        {
            return $"Name: {this.Name} - E-mail: {this.Email}";
        }

    }
}