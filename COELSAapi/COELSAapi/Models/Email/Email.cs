using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COELSAapi.Models
{
    public class Email
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required, Display(Name = "E-mail"), EmailAddress]
        public string EmailContact { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int Asunto { get; set; }        
        public string OtroAsunto { get; set; }
        [Required]
        public string Mensaje {
            get { return Mensaje; }
            set {
                if(this.Asunto != 0)
                {
                    Mensaje = string.Empty;
                }
                else
                {
                    Mensaje = value;
                }
            }
        }



    }
}