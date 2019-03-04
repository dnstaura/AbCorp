using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testuser.Models.SysModel;

namespace testuser.Controllers.SysControllers
{
    public class tblArchivosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblArchivos
        [Authorize]
        public ActionResult Index()
        {
            var tblArchivos = db.tblArchivos.Include(t => t.tblCasos);
            return View(tblArchivos.ToList());
        }

        // GET: tblArchivos/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivos tblArchivos = db.tblArchivos.Find(id);
            if (tblArchivos == null)
            {
                return HttpNotFound();
            }
            return View(tblArchivos);
        }

        // GET: tblArchivos/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso");
            return View();
        }

        // POST: tblArchivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Archivo,Id_Caso,Archivo,Descripción,Fecha_Agregado")] tblArchivos tblArchivos)
        {
            if (ModelState.IsValid)
            {
                db.tblArchivos.Add(tblArchivos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblArchivos.Id_Caso);
            return View(tblArchivos);
        }

        // GET: tblArchivos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivos tblArchivos = db.tblArchivos.Find(id);
            if (tblArchivos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblArchivos.Id_Caso);
            return View(tblArchivos);
        }

        // POST: tblArchivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Archivo,Id_Caso,Archivo,Descripción,Fecha_Agregado")] tblArchivos tblArchivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblArchivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblArchivos.Id_Caso);
            return View(tblArchivos);
        }

        // GET: tblArchivos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivos tblArchivos = db.tblArchivos.Find(id);
            if (tblArchivos == null)
            {
                return HttpNotFound();
            }
            return View(tblArchivos);
        }

        // POST: tblArchivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblArchivos tblArchivos = db.tblArchivos.Find(id);
            db.tblArchivos.Remove(tblArchivos);
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
