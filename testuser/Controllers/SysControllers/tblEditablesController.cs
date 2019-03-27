using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using testuser.Models.SysModel;
using testuser.Models;
using Microsoft.AspNet.Identity;

namespace testuser.Controllers.SysControllers
{
    public class tblEditablesController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblEditables
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Index()
        {
            var dato = db.tblEditables.Where(x => x.idEditable == x.idEditable).Count();
            ViewBag.ed = dato;
            return View(db.tblEditables.ToList());
        }

        // GET: tblEditables/Details/5
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEditables tblEditables = db.tblEditables.Find(id);
            if (tblEditables == null)
            {
                return HttpNotFound();
            }
            return View(tblEditables);
        }

        // GET: tblEditables/Create
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEditables/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEditable,fecha,entidad,instrumento,documento,tipo_documento,img,descripcion")] tblEditables tblEditables)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblEditables.img = "/Content/Editables/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Editables/") + img);

                    }
                    db.tblEditables.Add(tblEditables);
                    db.SaveChanges();

                    /*NOTIFICACION*/
                    ApplicationDbContext dbs = new ApplicationDbContext();
                    Notifications notificacion = new Notifications();
                    notificacion.Module = "Editables";
                    notificacion.Message = string.Format("Registro un nuevo archivo editable");
                    notificacion.Date = DateTime.Now;
                    notificacion.Viewed = false;
                    notificacion.Usuario_Id = User.Identity.GetUserId();

                    dbs.Notification.Add(notificacion);
                    dbs.SaveChanges();
                    /*FIN NOTIFICACION*/

                    return RedirectToAction("Index");
                }
            }

            return View(tblEditables);
        }

        // GET: tblEditables/Edit/5
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEditables tblEditables = db.tblEditables.Find(id);
            if (tblEditables == null)
            {
                return HttpNotFound();
            }
            return View(tblEditables);
        }

        // POST: tblEditables/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEditable,fecha,entidad,instrumento,documento,tipo_documento,img,descripcion")] tblEditables tblEditables)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblEditables.img = "/Content/Editables/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Editables/") + img);

                    }

                }
                db.Entry(tblEditables).State = EntityState.Modified;
                db.SaveChanges();

                /*NOTIFICACION*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Editables";
                notificacion.Message = string.Format("Edito un archivo editable");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*FIN NOTIFICACION*/

                return RedirectToAction("Index");
            }
            return View(tblEditables);
        }

        // GET: tblEditables/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEditables tblEditables = db.tblEditables.Find(id);
            if (tblEditables == null)
            {
                return HttpNotFound();
            }
            return View(tblEditables);
        }

        // POST: tblEditables/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEditables tblEditables = db.tblEditables.Find(id);
            db.tblEditables.Remove(tblEditables);
            db.SaveChanges();

            /*NOTIFICACION*/
            ApplicationDbContext dbs = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Editables";
            notificacion.Message = string.Format("Elimino un archivo editable");
            notificacion.Date = DateTime.Now;
            notificacion.Viewed = false;
            notificacion.Usuario_Id = User.Identity.GetUserId();

            dbs.Notification.Add(notificacion);
            dbs.SaveChanges();
            /*FIN NOTIFICACION*/

            return RedirectToAction("Index");
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
