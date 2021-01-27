using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COELSAapi.Models
{
    public partial class New
    {

        public New(string title, string link, string context, DateTime created_at)
        {
            this.Title = title;
            this.Link = link;
            this.Context = context;            
            this.Created_At = created_at;
            this.Updated_At = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Title: {this.Title} - Link: {this.Link}";
        }
    }
}