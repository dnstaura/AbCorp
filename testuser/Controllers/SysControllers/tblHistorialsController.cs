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
    public class tblHistorialsController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblHistorials
        [Authorize(Roles ="Administrador,Abogado,Observador")]
        public ActionResult Index(int? id)
        {
            /*Retorna lista de historiales*/
            var tblHistorial = db.tblHistorial.Include(t => t.tblCasos);


            /*Contador*/
            var dato = db.tblHistorial.Where(x => x.Id_Historial== x.Id_Historial).Count();
            ViewBag.his = dato;



            /*retorna lista de casos en historial*/
            var casoshistorial = db.tblHistorial.Where(x => x.Id_Caso == id).ToList();

            if (casoshistorial.Count > 0)
            {
            ViewBag.casehistory = casoshistorial;
            }
            else
            {

            }

            return View(tblHistorial.ToList());
        }


        // GET: tblHistorials/Details/5
        [Authorize(Roles = "Administrador,Abogado,Observador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            if (tblHistorial == null)
            {
                return HttpNotFound();
            }
            return View(tblHistorial);
        }

        // GET: tblHistorials/Create
        [Authorize(Roles = "Administrador,Abogado")]
        public ActionResult Create()
        {
            ViewData["Fecha_Agregado"] = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso");
            return View();
        }

        // POST: tblHistorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Historial,Id_Caso,Fecha_Inicio,Fecha_Final,Descripcion,Archivo,Fecha_Agregado")] tblHistorial tblHistorial)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblHistorial.Archivo = "/Content/Historial/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Historial/") + img);

                    }
                    db.tblHistorial.Add(tblHistorial);
                    db.SaveChanges();
                    /*NOTIFICACION*/
                    ApplicationDbContext dbs = new ApplicationDbContext();
                    Notifications notificacion = new Notifications();
                    notificacion.Module = "Historial";
                    notificacion.Message = string.Format("Registro un nuevo historial");
                    notificacion.Date = DateTime.Now;
                    notificacion.Viewed = false;
                    notificacion.Usuario_Id = User.Identity.GetUserId();

                    dbs.Notification.Add(notificacion);
                    dbs.SaveChanges();
                    /*FIN NOTIFICACION*/

                    return RedirectToAction("Index");
                }
              
            }

            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblHistorial.Id_Caso);
            return View(tblHistorial);
        }

        // GET: tblHistorials/Edit/5
        [Authorize(Roles = "Administrador,Abogado")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            if (tblHistorial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblHistorial.Id_Caso);
            return View(tblHistorial);
        }

        // POST: tblHistorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Historial,Id_Caso,Fecha_Inicio,Fecha_Final,Descripcion,Archivo,Fecha_Agregado")] tblHistorial tblHistorial)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblHistorial.Archivo = "/Content/Historial/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Historial/") + img);

                    }

                }
                db.Entry(tblHistorial).State = EntityState.Modified;
                db.SaveChanges();
                /*NOTIFICACION*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Historial";
                notificacion.Message = string.Format("Edito un historial");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*FIN NOTIFICACION*/
                return RedirectToAction("Index");
            }
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblHistorial.Id_Caso);
            return View(tblHistorial);
        }

        // GET: tblHistorials/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            if (tblHistorial == null)
            {
                return HttpNotFound();
            }
            return View(tblHistorial);
        }

        // POST: tblHistorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            db.tblHistorial.Remove(tblHistorial);
            db.SaveChanges();
            /*NOTIFICACION*/
            ApplicationDbContext dbs = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Historial";
            notificacion.Message = string.Format("Elimino un historial");
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
