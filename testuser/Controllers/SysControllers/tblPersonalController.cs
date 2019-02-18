﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class tblPersonalController : Controller
    {
        private dbModel db = new dbModel();
        

        // GET: tblPersonal
        public ActionResult Index()
        {

            var tblPersonal = db.tblPersonal.Include(t => t.tblCategoriaPersonal).Include(t => t.tblEstadosRegistros);

            //var test = db.tblPersonal.Where(x => x.Id_Personal == x.Id_Personal).Count();
            //ViewBag.pe = test;
            //var fechahoy = DateTime"2019-02-04";
            //var noti = db.tblPersonal.Where(modelItem => modelItem.Fecha_Nacimiento = fechahoy);

            //var notifacion = (from a in db.tblPersonal
            //                  where (a.Fecha_Nacimiento.Date == DateTime.Today)
            //                  select a.Nombres +" "+a.Apellidos  );
            //ViewBag.Notii = notifacion;

            //var today = DateTime.Today;
            //var q = db.tblPersonal.Where(t => DbFunctions.TruncateTime(t.Fecha_Nacimiento) >= today);

            return View(tblPersonal.ToList());

            
        }

        // GET: tblPersonal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPersonal tblPersonal = db.tblPersonal.Find(id);
            if (tblPersonal == null)
            {
                return HttpNotFound();
            }
            return View(tblPersonal);
        }

        // GET: tblPersonal/Create
        public ActionResult Create()
        {
            ViewBag.id_CategoriaPersonal = new SelectList(db.tblCategoriaPersonal, "id_CategoriaPersonal", "CategoriaPersonal");
            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado");
            return View();
        }

        // POST: tblPersonal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Personal,Nombres,Apellidos,redsocial,Telefono,Correo,Direccion,Fecha_Nacimiento,id_CategoriaPersonal,Id_EstadoRegistro,Fecha_Agregado")] tblPersonal tblPersonal)
        {
            if (ModelState.IsValid)
            {
                db.tblPersonal.Add(tblPersonal);
                db.SaveChanges();

                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Personal";
                notificacion.Message = string.Format("Nuevo personal registrado");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.id_CategoriaPersonal = new SelectList(db.tblCategoriaPersonal, "id_CategoriaPersonal", "CategoriaPersonal", tblPersonal.id_CategoriaPersonal);
            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado", tblPersonal.Id_EstadoRegistro);
            return View(tblPersonal);
        }

        // GET: tblPersonal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPersonal tblPersonal = db.tblPersonal.Find(id);
            if (tblPersonal == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_CategoriaPersonal = new SelectList(db.tblCategoriaPersonal, "id_CategoriaPersonal", "CategoriaPersonal", tblPersonal.id_CategoriaPersonal);
            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado", tblPersonal.Id_EstadoRegistro);
            return View(tblPersonal);
        }

        // POST: tblPersonal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Personal,Nombres,Apellidos,redsocial,Telefono,Correo,Direccion,Fecha_Nacimiento,id_CategoriaPersonal,Id_EstadoRegistro,Fecha_Agregado")] tblPersonal tblPersonal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPersonal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_CategoriaPersonal = new SelectList(db.tblCategoriaPersonal, "id_CategoriaPersonal", "CategoriaPersonal", tblPersonal.id_CategoriaPersonal);
            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado", tblPersonal.Id_EstadoRegistro);
            return View(tblPersonal);
        }

        // GET: tblPersonal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPersonal tblPersonal = db.tblPersonal.Find(id);
            if (tblPersonal == null)
            {
                return HttpNotFound();
            }
            return View(tblPersonal);
        }

        // POST: tblPersonal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPersonal tblPersonal = db.tblPersonal.Find(id);
            db.tblPersonal.Remove(tblPersonal);
            db.SaveChanges();
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