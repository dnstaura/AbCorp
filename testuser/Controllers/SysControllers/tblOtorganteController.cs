﻿using Microsoft.AspNet.Identity;
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
    public class tblOtorganteController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblOtorgante
        [Authorize(Roles = "Administrador,Notario,digitadornotario,Observador")]
        public ActionResult Index()
        {
            var dato = db.tblOtorgante.Where(x => x.idotorgante == x.idotorgante).Count();
            ViewBag.ot = dato;
            return View(db.tblOtorgante.ToList());
        }

        // GET: tblOtorgante/Details/5
        [Authorize(Roles = "Administrador,Notario,digitadornotario,Observador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOtorgante tblOtorgante = db.tblOtorgante.Find(id);
            if (tblOtorgante == null)
            {
                return HttpNotFound();
            }
            return View(tblOtorgante);
        }

        // GET: tblOtorgante/Create
        [Authorize(Roles = "Administrador,Notario,digitadornotario,Observador")]
        public ActionResult Create()
        {
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View();
        }

        // POST: tblOtorgante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Notario,digitadornotario,Observador")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idotorgante,nombres,apellidos,redsocial,telefono,correo,fechanacimiento,lugarnacimiento,Id_Municipio,Id_Departamento")] tblOtorgante tblOtorgante)
        {
            if (ModelState.IsValid)
            {
                db.tblOtorgante.Add(tblOtorgante);
                db.SaveChanges();

                /*NOTIFICACION*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Otorgantes";
                notificacion.Message = string.Format("Registro un nuevo Otorgante");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*FIN NOTIFICACION*/

                return RedirectToAction("Index");
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View(tblOtorgante);
        }

        // GET: tblOtorgante/Edit/5
        [Authorize(Roles = "Administrador,Notario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOtorgante tblOtorgante = db.tblOtorgante.Find(id);
            if (tblOtorgante == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View(tblOtorgante);
        }

        // POST: tblOtorgante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idotorgante,nombres,apellidos,redsocial,telefono,correo,fechanacimiento,lugarnacimiento,Id_Municipio,Id_Departamento")] tblOtorgante tblOtorgante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblOtorgante).State = EntityState.Modified;
                db.SaveChanges();

                /*NOTIFICACION*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Otorgantes";
                notificacion.Message = string.Format("Edito un Otorgante");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*FIN NOTIFICACION*/

                return RedirectToAction("Index");
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View(tblOtorgante);
        }

        // GET: tblOtorgante/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOtorgante tblOtorgante = db.tblOtorgante.Find(id);
            if (tblOtorgante == null)
            {
                return HttpNotFound();
            }
            return View(tblOtorgante);
        }

        // POST: tblOtorgante/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblOtorgante tblOtorgante = db.tblOtorgante.Find(id);
            db.tblOtorgante.Remove(tblOtorgante);
            db.SaveChanges();

            /*NOTIFICACION*/
            ApplicationDbContext dbs = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Otorgantes";
            notificacion.Message = string.Format("Elimino un Otorgante");
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
