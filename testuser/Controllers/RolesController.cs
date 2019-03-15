using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using testuser.Models;

namespace testuser.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult ListaRoles()
        {
            var roles = db.Roles.ToList();

            List<RoleView> roleView = new List<RoleView>();

            foreach (var item in roles)
            {
                var rol = new RoleView
                {
                    Name = item.Name,
                    RoleID = item.Id
                };

                roleView.Add(rol);
            }
            return View(roleView);
        }

        [Authorize]
        public ActionResult CrearRol()
        {
            /*Listado de roles a mostrar*/
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Administrador", Value = "Administrador" },
                new SelectListItem { Text = "Prueba", Value = "Prueba" },

            };
            ViewBag.show = ObjList.ToList();

            return PartialView();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CrearRol(RoleView rol)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists(rol.Name))
            {
                roleManager.Create(new IdentityRole(rol.Name));

                /*Notificacion*/
                ApplicationDbContext db = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Roles";
                notificacion.Message = string.Format("Creo un rol correctamente");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                db.Notification.Add(notificacion);
                db.SaveChanges();
                /*Fin Notificacion*/

                return RedirectToAction("ListaRoles");
            }
            return View(rol);
            
        }

        //[Authorize]
        //public ActionResult EditarRol(string id)
        //{
        //    var rol = (from r in db.Roles
        //               where r.Id == id
        //               select r).FirstOrDefault();

        //    var rolView = new RoleView();
        //    rolView.RoleID = rol.Id;
        //    rolView.Name = rol.Name;

        //    return PartialView(rolView);
        //}

        //[HttpPost]
        //public ActionResult EditarRol(RoleView rolView)
        //{
        //    //try
        //    //{
        //    //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        //    //    roleManager.Update(rolView);

        //    //    db.Entry(rolView).State = EntityState.Modified;
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("ListaRoles");
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    throw;
        //    //}
        //}

        //[Authorize]
        //public ActionResult EliminarRol(string id)
        //{
        //    var rol = (from r in db.Roles
        //               where r.Id == id
        //               select r).FirstOrDefault();

        //    var rolView = new RoleView();
        //    rolView.RoleID = rol.Id;
        //    rolView.Name = rol.Name;

        //    return PartialView(rolView);
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult EliminarRol(RoleView rolView, string id)
        //{
        //    try
        //    {
        //        var rol = (from r in db.Roles
        //                   where r.Id == id
        //                   select r).FirstOrDefault();

        //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        //        {
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("_eliminarRol", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("RolId", rol.Id));


        //            cmd.ExecuteScalar();

        //            return RedirectToAction("ListaRoles");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        }
    }
}