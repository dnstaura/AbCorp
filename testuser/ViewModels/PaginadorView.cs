using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.ViewModels
{
    public class PaginadorView
    {
        public int PaginaActual { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistrosPorPaginas { get; set; }
    }
}