using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testuser.Models;

namespace testuser.ViewModels
{
    public class FuncionesEstaticas
    {
        public string Imagen { get; set; }
        public string FechaRegistro { get; set; }
        public string Nombre { get; set; }

        public static FuncionesEstaticas ObtenerHeader(string userID)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var usuario = db.Users.Find(userID);

                var Header = new FuncionesEstaticas();
                Header.Imagen = usuario.Imagen;
                Header.FechaRegistro = string.Format("{0:dd/MM/yyy}", usuario.Fecha);
                Header.Nombre = usuario.Nombres;

                return Header;
            }
        }
    }
}