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
    public class tblOtorganteController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblOtorgante
        [Authorize]
        public ActionResult Index()
        {
            var dato = db.tblOtorgante.Where(x => x.idotorgante == x.idotorgante).Count();
            ViewBag.ot = dato;
            return View(db.tblOtorgante.ToList());
        }

        // GET: tblOtorgante/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblOtorgante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idotorgante,nombres,apellidos,redsocial,telefono,correo,fechanacimiento,lugarnacimiento,municipio,departamento")] tblOtorgante tblOtorgante)
        {
            if (ModelState.IsValid)
            {
                db.tblOtorgante.Add(tblOtorgante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblOtorgante);
        }

        // GET: tblOtorgante/Edit/5
        [Authorize]
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
            return View(tblOtorgante);
        }

        // POST: tblOtorgante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idotorgante,nombres,apellidos,redsocial,telefono,correo,fechanacimiento,lugarnacimiento,municipio,departamento")] tblOtorgante tblOtorgante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblOtorgante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblOtorgante);
        }

        // GET: tblOtorgante/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
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

        // POST: tblOtorgante/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblOtorgante tblOtorgante = db.tblOtorgante.Find(id);
            db.tblOtorgante.Remove(tblOtorgante);
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
