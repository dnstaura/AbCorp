﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using testuser.Models.SysModel;
using System.Data.Entity.SqlServer;
using testuser.Models;
using Microsoft.AspNet.Identity;

namespace testuser.Controllers.SysControllers
{
    public class tblLibrosController : Controller
    {

        private dbModel db = new dbModel();

        // GET: tblLibros
        [Authorize]
        public ActionResult Index()
        {
            var tblLibros = db.tblLibros.Include(t => t.tblPersonal);

            //Contador Libros
            var libcount = (from a in db.tblLibros
                           where a.idlibros == a.idlibros
                           select a).Count();

            ViewBag.lb = libcount;

            return View(tblLibros.ToList());
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(string txtBuscar)
        {
            var buscar = (from a in db.tblLibros
                          where a.libro.Contains(txtBuscar) || a.tblPersonal.Nombres.Contains(txtBuscar)
                          select a).ToList();
            return View(buscar);
        }


        // GET: tblLibros/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLibros tblLibros = db.tblLibros.Find(id);
            if (tblLibros == null)
            {
                return HttpNotFound();
            }
            return View(tblLibros);
        }

        // GET: tblLibros/Create
        [Authorize]
        public ActionResult Create()
        {
            //ViewBag.idfavorde = new SelectList(db.tblAfavorde, "idfavorde", "nombres");
            //ViewBag.idotorgante = new SelectList(db.tblOtorgante, "idotorgante", "nombres");
            //ViewBag.id_Personal = new SelectList(db.tblPersonal, "Id_Personal", "Nombres");
            //ViewBag.id_Personal = new SelectList(db.tblPersonal.Join(
            //                    db.tblCategoriaPersonal,
            //                    a => a.Id_Personal,
            //                    b => b.id_CategoriaPersonal,
            //                    (v, s) =>
            //                     new
            //                     {
            //                         v = v,
            //                         s = s
            //                     }
            //               )
            //               .Select(
            //                temp0 =>
            //                new
            //                {
            //                    v = temp0.v,
            //                    s = temp0.s
            //                }
            //                ).Select(m => new SelectListItem
            //                {
            //                    Value = SqlFunctions.StringConvert((double)m.s.id_CategoriaPersonal).Trim(),
            //                    Text = m.v.Nombres + " - " + m.s.CategoriaPersonal
            //                }), "Value", "Text", 0);

            var notarios = (from p in db.tblPersonal
                            join c in db.tblCategoriaPersonal on p.id_CategoriaPersonal equals c.id_CategoriaPersonal
                            where c.CategoriaPersonal == "Notario"
                            select p).ToList();


        ViewBag.id_Personal = new SelectList(notarios, "Id_Personal", "Nombres");
            return View();
        }

        // POST: tblLibros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idlibros,fecha,correlativo,instrumento,folios,libro,img,otorgante,afavor,id_Personal")] tblLibros tblLibros)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblLibros.img = "/Content/Libros/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Libros/") + img);

                    }
                    db.tblLibros.Add(tblLibros);
                    db.SaveChanges();

                    /*NOTIFICACION*/
                    ApplicationDbContext dbs = new ApplicationDbContext();
                    Notifications notificacion = new Notifications();
                    notificacion.Module = "Libros";
                    notificacion.Message = string.Format("Registro un nuevo libro");
                    notificacion.Date = DateTime.Now;
                    notificacion.Viewed = false;
                    notificacion.Usuario_Id = User.Identity.GetUserId();

                    dbs.Notification.Add(notificacion);
                    dbs.SaveChanges();
                    /*FIN NOTIFICACION*/

                    return RedirectToAction("Index");
                }
            }

            //ViewBag.idfavorde = new SelectList(db.tblAfavorde, "idfavorde", "nombres", tblLibros.idfavorde);
            //ViewBag.idotorgante = new SelectList(db.tblOtorgante, "idotorgante", "nombres", tblLibros.idotorgante);
            ViewBag.id_Personal = new SelectList(db.tblPersonal, "Id_Personal", "Nombres", tblLibros.id_Personal);
         


            return View(tblLibros);
        }

        // GET: tblLibros/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLibros tblLibros = db.tblLibros.Find(id);
            if (tblLibros == null)
            {
                return HttpNotFound();
            }
            //ViewBag.idfavorde = new SelectList(db.tblAfavorde, "idfavorde", "nombres", tblLibros.idfavorde);
            //ViewBag.idotorgante = new SelectList(db.tblOtorgante, "idotorgante", "nombres", tblLibros.idotorgante);
            ViewBag.id_Personal = new SelectList(db.tblPersonal, "Id_Personal", "Nombres", tblLibros.id_Personal);
            return View(tblLibros);
        }

        // POST: tblLibros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idlibros,fecha,correlativo,instrumento,folios,libro,img,otorgante,afavor,id_Personal")] tblLibros tblLibros)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblLibros.img = "/Content/Libros/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Libros/") + img);

                    }
                
                }

                db.Entry(tblLibros).State = EntityState.Modified;
                db.SaveChanges();

                /*NOTIFICACION*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Libros";
                notificacion.Message = string.Format("Edito un libro");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*FIN NOTIFICACION*/

                return RedirectToAction("Index");
            }
            //ViewBag.idfavorde = new SelectList(db.tblAfavorde, "idfavorde", "nombres", tblLibros.idfavorde);
            //ViewBag.idotorgante = new SelectList(db.tblOtorgante, "idotorgante", "nombres", tblLibros.idotorgante);
            ViewBag.id_Personal = new SelectList(db.tblPersonal, "Id_Personal", "Nombres", tblLibros.id_Personal);
            return View(tblLibros);
        }

        // GET: tblLibros/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLibros tblLibros = db.tblLibros.Find(id);
            if (tblLibros == null)
            {
                return HttpNotFound();
            }
            return View(tblLibros);
        }

        // POST: tblLibros/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLibros tblLibros = db.tblLibros.Find(id);
            db.tblLibros.Remove(tblLibros);
            db.SaveChanges();

            /*NOTIFICACION*/
            ApplicationDbContext dbs = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Libros";
            notificacion.Message = string.Format("Elimino un libro");
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
