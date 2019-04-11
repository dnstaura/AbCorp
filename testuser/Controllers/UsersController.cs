using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testuser.Models;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using testuser.ViewModels;

namespace testuser.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        [Authorize(Roles="Administrador")]
        public ActionResult Index(int pagina = 1)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var rollist = roleManager.Roles.ToList();

            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.Nombres,
                    Estado = user.Estado,
                    UserID = user.Id
                };
                usersView.Add(userView);
            }
            
            /*Contadores*/

            /*Usuarios registrados*/
            var Usuarios = usersView.Where(x => x.UserID == x.UserID).Count();
            ViewBag.Users = Usuarios;
            /*Fin Usuarios registrados*/

            /*Usuarios Activos*/
            var usac = users.Where(x => x.Estado == true).Count();
            ViewBag.UA = usac;
            /*Fin Usuarios Activos*/

            /*Usuarios Inactivos*/
            var usin = users.Where(x => x.Estado == false).Count();
            ViewBag.UI = usin;
            /*Fin Usuarios Inactivos*/


            if (roleManager.RoleExists("Administrador"))
            {
                /*Usuarios con rol administrador*/
                var usad = rollist.Where(x => x.Name == "Administrador").First();
                var usadJoin = db.UserRoles.Where(x => x.RoleId == usad.Id).Count();
                ViewBag.Ad = usadJoin;
                /*Fin Usuarios con rol administrador*/
            }

            if (roleManager.RoleExists("Notario"))
            {
                /*Usuarios con rol Notario*/
                var usno = rollist.Where(x => x.Name == "Notario").First();
                var usnoJoin = db.UserRoles.Where(x => x.RoleId == usno.Id).Count();
                ViewBag.No = usnoJoin;
                /*Fin Usuarios con rol Notario*/
            }
            if (roleManager.RoleExists("digitadornotario"))
            {
                /*Usuarios con rol Digitador Notario*/
                var usdi = rollist.Where(x => x.Name == "digitadornotario").First();
                var usdiJoin = db.UserRoles.Where(x => x.RoleId == usdi.Id).Count();
                ViewBag.UsDi = usdiJoin;
                /*Fin Usuarios con rol Digitador Notario*/
            }

            if (roleManager.RoleExists("Abogado"))
            {

                /*Usuarios con rol Abogacia*/
                var usab = rollist.Where(x => x.Name == "Abogado").First();
                var usabJoin = db.UserRoles.Where(x => x.RoleId == usab.Id).Count();
                ViewBag.Ab = usabJoin;
                /*Fin Usuarios con rol Abogacia*/
            }
            if (roleManager.RoleExists("digitadorabogado"))
            {

                /*Usuarios con rol Abogacia*/
                var usdiab = rollist.Where(x => x.Name == "digitadorabogado").First();
                var usdiabJoin = db.UserRoles.Where(x => x.RoleId == usdiab.Id).Count();
                ViewBag.DiAb = usdiabJoin;
                /*Fin Usuarios con rol Abogacia*/
            }
            if (roleManager.RoleExists("Observador"))
            {

                /*Usuarios con rol Secretaria*/
                var usse = rollist.Where(x => x.Name == "Observador").First();
                var usseJoin = db.UserRoles.Where(x => x.RoleId == usse.Id).Count();
                ViewBag.Ob = usseJoin;
                /*Fin Usuarios con rol secretaria*/
            }

            /*Fin Contadores*/
        /*Paginador*/
            var cantidadRegistrosPorPagina = 8;

            var usuarios = db.Users.OrderBy(u => u.Fecha)
                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();
            var totalDeRegistros = db.Users.Count();

            var modelo = new ListasView();
            modelo.Usuarios = usuarios;
            modelo.PaginaActual = pagina;
            modelo.TotalRegistros = totalDeRegistros;
            modelo.RegistrosPorPaginas = cantidadRegistrosPorPagina;
            /*Fin Paginador*/
            return View(modelo);
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string busqueda)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            var cantidadRegistrosPorPagina = 8;

            int pagina = 1;

            var usuarios = (from a in users
                            where a.Email.Contains(busqueda) || a.Nombres.Contains(busqueda)
                            select a)
                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();
            var totalDeRegistros = db.Users.Count();

            var modelo = new ListasView();
            modelo.Usuarios = usuarios;
            modelo.PaginaActual = pagina;
            modelo.TotalRegistros = totalDeRegistros;
            modelo.RegistrosPorPaginas = cantidadRegistrosPorPagina;

            /*Contadores*/
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var rollist = roleManager.Roles.ToList();

            var userss = userManager.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in userss)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.Nombres,
                    Estado = user.Estado,
                    UserID = user.Id
                };
                usersView.Add(userView);
            }

            /*Contadores*/

            /*Usuarios registrados*/
            var Usuarios = usersView.Where(x => x.UserID == x.UserID).Count();
            ViewBag.Users = Usuarios;
            /*Fin Usuarios registrados*/

            /*Usuarios Activos*/
            var usac = userss.Where(x => x.Estado == true).Count();
            ViewBag.UA = usac;
            /*Fin Usuarios Activos*/

            /*Usuarios Inactivos*/
            var usin = userss.Where(x => x.Estado == false).Count();
            ViewBag.UI = usin;
            /*Fin Usuarios Inactivos*/


            if (roleManager.RoleExists("Administrador"))
            {
                /*Usuarios con rol administrador*/
                var usad = rollist.Where(x => x.Name == "Administrador").First();
                var usadJoin = db.UserRoles.Where(x => x.RoleId == usad.Id).Count();
                ViewBag.Ad = usadJoin;
                /*Fin Usuarios con rol administrador*/
            }

            if (roleManager.RoleExists("Notario"))
            {
                /*Usuarios con rol Notario*/
                var usno = rollist.Where(x => x.Name == "Notario").First();
                var usnoJoin = db.UserRoles.Where(x => x.RoleId == usno.Id).Count();
                ViewBag.No = usnoJoin;
                /*Fin Usuarios con rol Notario*/
            }

            if (roleManager.RoleExists("Abogacia"))
            {

                /*Usuarios con rol Abogacia*/
                var usab = rollist.Where(x => x.Name == "Abogacia").First();
                var usabJoin = db.UserRoles.Where(x => x.RoleId == usab.Id).Count();
                ViewBag.Ab = usabJoin;
                /*Fin Usuarios con rol Abogacia*/
            }
            if (roleManager.RoleExists("Secretaria"))
            {

                /*Usuarios con rol Secretaria*/
                var usse = rollist.Where(x => x.Name == "Secretaria").First();
                var usseJoin = db.UserRoles.Where(x => x.RoleId == usse.Id).Count();
                ViewBag.Se = usseJoin;
                /*Fin Usuarios con rol secretaria*/
            }

            /*Fin Contadores*/
            /*Paginador*/

            return View(modelo);

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Roles(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.Nombres,
                Estado = user.Estado,
                UserID = user.Id,
                Roles = rolesView
            };

            return View(userView);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.Nombres,
                Estado = user.Estado,
                UserID = user.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Seleccione un rol...]" });
            list = list.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name");

            return View(userView);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            var roleID = Request["RoleID"];

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.Nombres,
                Estado = user.Estado,
                UserID = user.Id
            };

            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.Error = "Debe seleccionar un rol";



                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Select a rol...]" });
                list = list.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name");

                return View(userView);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == roleID);
            if (!userManager.IsInRole(userID, role.Name))
            {
                userManager.AddToRole(userID, role.Name);
            }

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            userView = new UserView
            {
                Email = user.Email,
                Name = user.Nombres,
                Estado = user.Estado,
                UserID = user.Id,
                Roles = rolesView
            };

            ApplicationDbContext dba = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Usuarios";
            notificacion.Message = string.Format("Asigno un rol correctamente");
            notificacion.Date = DateTime.Now;
            notificacion.Viewed = false;
            notificacion.Usuario_Id = User.Identity.GetUserId();

            dba.Notification.Add(notificacion);
            dba.SaveChanges();

            return View("Roles", userView);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.Users.ToList().Find(u => u.Id == userID);
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleID);

            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.Nombres,
                Estado = user.Estado,
                UserID = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}