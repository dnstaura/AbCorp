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
    public class tblEstadosRegistrosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblEstadosRegistros
        [Authorize(Roles = "Administrador,Abogado,Notario,Observador")]
        public ActionResult Index()
        {
            return View(db.tblEstadosRegistros.ToList());
        }

        // GET: tblEstadosRegistros/Details/5
        [Authorize(Roles = "Administrador,Abogado,Notario,Observador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadosRegistros tblEstadosRegistros = db.tblEstadosRegistros.Find(id);
            if (tblEstadosRegistros == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadosRegistros);
        }

        // GET: tblEstadosRegistros/Create
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEstadosRegistros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EstadoRegistro,Estado")] tblEstadosRegistros tblEstadosRegistros)
        {
            if (ModelState.IsValid)
            {
                db.tblEstadosRegistros.Add(tblEstadosRegistros);
                db.SaveChanges();

                /*Notificacion*/
                ApplicationDbContext bd = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Estado de registro Cliente/Personal";
                notificacion.Message = string.Format("Registro un nuevo estado");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                bd.Notification.Add(notificacion);
                bd.SaveChanges();
                /*Fin Notificacion*/

                return RedirectToAction("Index");
            }

            return View(tblEstadosRegistros);
        }

        // GET: tblEstadosRegistros/Edit/5
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadosRegistros tblEstadosRegistros = db.tblEstadosRegistros.Find(id);
            if (tblEstadosRegistros == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadosRegistros);
        }

        // POST: tblEstadosRegistros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EstadoRegistro,Estado")] tblEstadosRegistros tblEstadosRegistros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEstadosRegistros).State = EntityState.Modified;
                db.SaveChanges();

                /*Notificacion*/
                ApplicationDbContext bd = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Estado de registro Cliente/Personal";
                notificacion.Message = string.Format("Edito un estado Cliente/Personal");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                bd.Notification.Add(notificacion);
                bd.SaveChanges();
                /*Fin Notificacion*/

                return RedirectToAction("Index");
            }
            return View(tblEstadosRegistros);
        }

        // GET: tblEstadosRegistros/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadosRegistros tblEstadosRegistros = db.tblEstadosRegistros.Find(id);
            if (tblEstadosRegistros == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadosRegistros);
        }

        // POST: tblEstadosRegistros/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEstadosRegistros tblEstadosRegistros = db.tblEstadosRegistros.Find(id);
            db.tblEstadosRegistros.Remove(tblEstadosRegistros);
            db.SaveChanges();

            /*Notificacion*/
            ApplicationDbContext bd = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Estado de registro Cliente/Personal";
            notificacion.Message = string.Format("Elimino un estado Cliente/Personal");
            notificacion.Date = DateTime.Now;
            notificacion.Viewed = false;
            notificacion.Usuario_Id = User.Identity.GetUserId();

            bd.Notification.Add(notificacion);
            bd.SaveChanges();
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
