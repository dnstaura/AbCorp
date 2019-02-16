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
    public class tblEstadosCasosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblEstadosCasos
        public ActionResult Index()
        {
            return View(db.tblEstadosCasos.ToList());
        }

        // GET: tblEstadosCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadosCasos tblEstadosCasos = db.tblEstadosCasos.Find(id);
            if (tblEstadosCasos == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadosCasos);
        }

        // GET: tblEstadosCasos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEstadosCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EstadoCaso,Nombre_Estado")] tblEstadosCasos tblEstadosCasos)
        {
            if (ModelState.IsValid)
            {
                db.tblEstadosCasos.Add(tblEstadosCasos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEstadosCasos);
        }

        // GET: tblEstadosCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadosCasos tblEstadosCasos = db.tblEstadosCasos.Find(id);
            if (tblEstadosCasos == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadosCasos);
        }

        // POST: tblEstadosCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EstadoCaso,Nombre_Estado")] tblEstadosCasos tblEstadosCasos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEstadosCasos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEstadosCasos);
        }

        // GET: tblEstadosCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadosCasos tblEstadosCasos = db.tblEstadosCasos.Find(id);
            if (tblEstadosCasos == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadosCasos);
        }

        // POST: tblEstadosCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEstadosCasos tblEstadosCasos = db.tblEstadosCasos.Find(id);
            db.tblEstadosCasos.Remove(tblEstadosCasos);
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
