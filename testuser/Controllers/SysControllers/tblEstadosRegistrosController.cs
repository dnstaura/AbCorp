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
    public class tblEstadosRegistrosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblEstadosRegistros
        public ActionResult Index()
        {
            return View(db.tblEstadosRegistros.ToList());
        }

        // GET: tblEstadosRegistros/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEstadosRegistros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EstadoRegistro,Estado")] tblEstadosRegistros tblEstadosRegistros)
        {
            if (ModelState.IsValid)
            {
                db.tblEstadosRegistros.Add(tblEstadosRegistros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEstadosRegistros);
        }

        // GET: tblEstadosRegistros/Edit/5
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EstadoRegistro,Estado")] tblEstadosRegistros tblEstadosRegistros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEstadosRegistros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEstadosRegistros);
        }

        // GET: tblEstadosRegistros/Delete/5
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEstadosRegistros tblEstadosRegistros = db.tblEstadosRegistros.Find(id);
            db.tblEstadosRegistros.Remove(tblEstadosRegistros);
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
