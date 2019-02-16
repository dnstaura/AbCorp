using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testuser.Models;

namespace testuser.ViewModels
{
    public class ListasView: PaginadorView
    {
        public List<ApplicationUser> Usuarios { get; set; }
    }
}