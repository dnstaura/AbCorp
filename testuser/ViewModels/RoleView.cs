using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace testuser.Models
{
    public class RoleView
    {
        public string RoleID { get; set; }

        [Display(Name = "Rol")]
        public string Name { get; set; }
    }
}