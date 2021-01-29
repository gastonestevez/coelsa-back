using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace COELSAapi.Models
{
    public class UserData
    {        
        public UserData()
        {
            this.Created_At = DateTime.Now;
        }
        [DataMember]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [DataMember]
        [MaxLength(150)]
        public string Company { get; set; }

        /// <summary>
        /// Indica el Rol de usuario. 1=Administrador, 2=Gestor de novedades
        /// </summary>
        [DataMember]
        [Required]
        [Range(1, 2)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Role debe ser númerico y debe estar comprendido entre 1 (Administrador) y 2 (Gestor de novedades)")]
        public int Role { get; set; }

        [DataMember]
        [Required]        
        public string Password { get; set; }

        [DataMember]
        [Required,EmailAddress]
        [MaxLength(250)]
        public string Email { get; set; }
        
        public DateTime Updated_At { get; set; }

        public DateTime Created_At { get; set; }
    }
}