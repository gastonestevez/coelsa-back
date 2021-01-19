using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COELSAapi.Models
{
    [MetadataType(typeof(MetaData))]
    public partial class User
    {        
        public User()
        {
            this.Created_At = DateTime.Now;
            this.Updated_At = DateTime.Now;
        }
        public User(string name,string email,string password,int role) : this()
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;                        
        }        

        public override string ToString()
        {
            return $"Name: {this.Name} - E-mail: {this.Email}";
        }        

        sealed class MetaData
        {
            [Required]
            public string Name;
            [Required]
            public string Email;
            [Required]
            public string Password;
            [Required]
            public int Role;   
            public DateTime Created_At { get; private set; }
            public DateTime Updated_At { get; private set; }
            public bool Marcado { get; private set; }
            
    
        }
    }
}