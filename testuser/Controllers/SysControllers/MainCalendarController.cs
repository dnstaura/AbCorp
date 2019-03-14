using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testuser.Models;
using testuser.Models.SysModel;

namespace testuser.Controllers.SysControllers
{
    public class MainCalendarController : Controller
    {
        dbModel db = new dbModel();
        // GET: MainEventCalendar
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Indexx()
        {

            var dato = db.tblEventCalendar;

            return Json(dato.AsEnumerable().Select(a => new {
                id_Event = a.id_Event,
                title = a.title,
                descripcion = a.descripcion,
                color = a.color,
                textColor = a.textColor,
                start = a.start.ToUniversalTime().ToString("yyyy.MM.dd HH:mm:ss.fff"),
                end = a.end.ToUniversalTime().ToString("yyyy.MM.dd HH:mm:ss.fff")
            }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Indexx(int txtID, string btnAgregar, string btnEditar, string btnEliminar, string txtFechaInicio, string txtFechaFin, string txtTitulo, string txtHora, string txtDescripcion, string txtColor, tblEventCalendar ev)
        {
            if (Request.HttpMethod == "POST")
            {
                if (btnAgregar == "agregar")
                {
                    var datosevento = new tblEventCalendar
                    {
                        id_Event = txtID,
                        title = txtTitulo,
                        descripcion = txtDescripcion,
                        color = txtColor,
                        textColor = "#FFFFFF",
                        start = Convert.ToDateTime(txtFechaInicio + " " + txtHora),
                        end = Convert.ToDateTime(txtFechaFin + " " + txtHora)

                    };
                    db.tblEventCalendar.Add(datosevento);
                    db.SaveChanges();

                    /*NOTIFICACION*/
                    ApplicationDbContext dbs = new ApplicationDbContext();
                    Notifications notificacion = new Notifications();
                    notificacion.Module = "Eventos";
                    notificacion.Message = string.Format("Registro un nuevo evento");
                    notificacion.Date = DateTime.Now;
                    notificacion.Viewed = false;
                    notificacion.Usuario_Id = User.Identity.GetUserId();

                    dbs.Notification.Add(notificacion);
                    dbs.SaveChanges();
                    /*FIN NOTIFICACION*/

                    return RedirectToAction("Index", "Home");
                }
                else if (btnEditar == "editar")
                {

                    var datosevento = new tblEventCalendar
                    {
                        id_Event = txtID,
                        title = txtTitulo,
                        descripcion = txtDescripcion,
                        color = txtColor,
                        textColor = "#FFFFFF",
                        start = Convert.ToDateTime(txtFechaInicio),
                        end = Convert.ToDateTime(txtFechaFin)
                    };
                    db.tblEventCalendar.Attach(datosevento);
                    db.Entry(datosevento).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    /*NOTIFICACION*/
                    ApplicationDbContext dbs = new ApplicationDbContext();
                    Notifications notificacion = new Notifications();
                    notificacion.Module = "Eventos";
                    notificacion.Message = string.Format("Edito un evento");
                    notificacion.Date = DateTime.Now;
                    notificacion.Viewed = false;
                    notificacion.Usuario_Id = User.Identity.GetUserId();

                    dbs.Notification.Add(notificacion);
                    dbs.SaveChanges();
                    /*FIN NOTIFICACION*/

                    return RedirectToAction("Index", "Home");
                }
                else if (btnEliminar == "eliminar")
                {
                    var datosevento = new tblEventCalendar
                    {
                        id_Event = txtID,
                        title = txtTitulo,
                        descripcion = txtDescripcion,
                        color = txtColor,
                        textColor = "#FFFFFF",
                        start = Convert.ToDateTime(txtFechaInicio),
                        end = Convert.ToDateTime(txtFechaFin)
                    };
                    db.tblEventCalendar.Attach(datosevento);
                    db.tblEventCalendar.Remove(datosevento);
                    db.SaveChanges();

                    /*NOTIFICACION*/
                    ApplicationDbContext dbs = new ApplicationDbContext();
                    Notifications notificacion = new Notifications();
                    notificacion.Module = "Eventos";
                    notificacion.Message = string.Format("Elimino un evento");
                    notificacion.Date = DateTime.Now;
                    notificacion.Viewed = false;
                    notificacion.Usuario_Id = User.Identity.GetUserId();

                    dbs.Notification.Add(notificacion);
                    dbs.SaveChanges();
                    /*FIN NOTIFICACION*/

                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Index", "Home");



        }

    }
}