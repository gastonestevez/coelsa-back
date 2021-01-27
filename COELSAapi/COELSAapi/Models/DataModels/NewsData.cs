using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace COELSAapi.Models.DataModels
{
    public class NewsData
    {

        public NewsData()
        {
            this.Created_At = DateTime.Now;
        }

        [DataMember]
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [DataMember]
        [Required]
        [MaxLength(255)]
        public string Link { get; set; }

        [DataMember]
        [Required]
        public string Context { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }        
    }
}