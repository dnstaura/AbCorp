using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace testuser.Models
{
    public class UserView
    {
        public string UserID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Estado { get; set; }

        public RoleView Role { get; set; }

        public List<RoleView> Roles { get; set; }
    }
}