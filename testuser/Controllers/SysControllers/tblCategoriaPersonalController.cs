using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testuser.Models;
using testuser.Models.SysModel;

namespace testuser.Controllers.SysControllers
{
    public class tblCategoriaPersonalController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblCategoriaPersonal
        [Authorize(Roles = "Administrador,Abogado,Notario,Observador")]
        public ActionResult Index()
        {
            return View(db.tblCategoriaPersonal.ToList());
        }

        // GET: tblCategoriaPersonal/Details/5
        [Authorize(Roles = "Administrador,Abogado,Notario,Observador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoriaPersonal tblCategoriaPersonal = db.tblCategoriaPersonal.Find(id);
            if (tblCategoriaPersonal == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoriaPersonal);
        }

        // GET: tblCategoriaPersonal/Create
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblCategoriaPersonal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_CategoriaPersonal,CategoriaPersonal")] tblCategoriaPersonal tblCategoriaPersonal)
        {
            if (ModelState.IsValid)
            {
                db.tblCategoriaPersonal.Add(tblCategoriaPersonal);
                db.SaveChanges();

                /*Notificacion*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Categoria personal";
                notificacion.Message = string.Format("Registro una nueva categoria");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*Fin Notificacion*/

                return RedirectToAction("Index");
            }

            return View(tblCategoriaPersonal);
        }

        // GET: tblCategoriaPersonal/Edit/5
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoriaPersonal tblCategoriaPersonal = db.tblCategoriaPersonal.Find(id);
            if (tblCategoriaPersonal == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoriaPersonal);
        }

        // POST: tblCategoriaPersonal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_CategoriaPersonal,CategoriaPersonal")] tblCategoriaPersonal tblCategoriaPersonal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCategoriaPersonal).State = EntityState.Modified;
                db.SaveChanges();

                /*Notificacion*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Categoria personal";
                notificacion.Message = string.Format("Edito una categoria registrada");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*Fin Notificacion*/

                return RedirectToAction("Index");
            }
            return View(tblCategoriaPersonal);
        }

        // GET: tblCategoriaPersonal/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoriaPersonal tblCategoriaPersonal = db.tblCategoriaPersonal.Find(id);
            if (tblCategoriaPersonal == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoriaPersonal);
        }

        // POST: tblCategoriaPersonal/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCategoriaPersonal tblCategoriaPersonal = db.tblCategoriaPersonal.Find(id);
            db.tblCategoriaPersonal.Remove(tblCategoriaPersonal);
            db.SaveChanges();

            /*Notificacion*/
            ApplicationDbContext dbs = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Categoria personal";
            notificacion.Message = string.Format("Elimino una categoria registrada");
            notificacion.Date = DateTime.Now;
            notificacion.Viewed = false;
            notificacion.Usuario_Id = User.Identity.GetUserId();

            dbs.Notification.Add(notificacion);
            dbs.SaveChanges();
            /*Fin Notificacion*/

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
