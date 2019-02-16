using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models
{
    public class Notifications
    {
        [Key]
        public int Id { get; set; }

        public string Module { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool Viewed { get; set; }


        public string Usuario_Id { get; set; }

        [ForeignKey("Usuario_Id")]
        public virtual ApplicationUser Usuario { get; set; }
    }
}