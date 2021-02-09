using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace COELSAapi.Models
{
    [DataContract]
    public class Email
    {

        public Email()
        {
            this.OtroAsunto = string.Empty;
            this.Asunto = 0;
        }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="nombre">Nombre</param>
        /// <param name="apellido">Apellido</param>
        /// <param name="emailContact">Email de contacto</param>
        /// <param name="telefono">Telefono</param>
        /// <param name="asunto">Asunto</param>
        /// <param name="otroAsunto">Otro Asunto</param>
        /// <param name="mensaje">Mensaje</param>
        public Email(string nombre, string apellido, string emailContact, string telefono, int asunto, string otroAsunto, string mensaje) : this()
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.EmailContact = emailContact;
            this.Telefono = telefono;
            this.Asunto = asunto;
            this.OtroAsunto = otroAsunto;
            this.Mensaje = mensaje;

        }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [DataMember]
        [Required, Display(Name = "E-mail"), EmailAddress]
        [MinLength(7)]
        [MaxLength(200)]
        public string EmailContact { get; set; }

        [DataMember]
        [Required]
        [MinLength(7)]
        [MaxLength(50)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Teléfono debe ser númerico")]
        public string Telefono { get; set; }


        /// <summary>
        /// Otro = 0;        
        /// Cheques = 1;
        /// Debitos = 2;
        /// Transferencias = 3;
        /// Accionistas = 4;
        /// Pago Directo = 5;
        /// Feriado Local = 6;       
        /// </summary>
        [DataMember]
        [Required]
        [Range(0,6)]        
        [RegularExpression("^[0-9]*$", ErrorMessage = "Asunto debe ser númerico y debe estar comprendido entre 0 y 6.")]        
        public int Asunto {get;set;}


        /// <summary>
        /// En caso de elegir Asunto = 0 (Otro). Debe completarse.
        /// </summary>
        [DataMember]
        [MaxLength(100)]
        public string OtroAsunto {
            get;set;
        }

        [DataMember]
        [Required]
        public string Mensaje { get; set; }


        #region PROPIEDADES EXTENDIDAS
        /// <summary>
        /// Muestra la descripcion del asunto
        /// </summary>
        public string AsuntoDescripcion { get { return UtilCodes.GetAsuntoEmail(this.Asunto); } }
        #endregion


    }
}