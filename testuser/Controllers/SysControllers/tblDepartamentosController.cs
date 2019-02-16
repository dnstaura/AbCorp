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
    public class tblDepartamentosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblDepartamentos
        public ActionResult Index()
        {
            return View(db.tblDepartamentos.ToList());
        }

        // GET: tblDepartamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDepartamentos tblDepartamentos = db.tblDepartamentos.Find(id);
            if (tblDepartamentos == null)
            {
                return HttpNotFound();
            }
            return View(tblDepartamentos);
        }

        // GET: tblDepartamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblDepartamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Departamento,Departamento")] tblDepartamentos tblDepartamentos)
        {
            if (ModelState.IsValid)
            {
                db.tblDepartamentos.Add(tblDepartamentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDepartamentos);
        }

        // GET: tblDepartamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDepartamentos tblDepartamentos = db.tblDepartamentos.Find(id);
            if (tblDepartamentos == null)
            {
                return HttpNotFound();
            }
            return View(tblDepartamentos);
        }

        // POST: tblDepartamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Departamento,Departamento")] tblDepartamentos tblDepartamentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDepartamentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDepartamentos);
        }

        // GET: tblDepartamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDepartamentos tblDepartamentos = db.tblDepartamentos.Find(id);
            if (tblDepartamentos == null)
            {
                return HttpNotFound();
            }
            return View(tblDepartamentos);
        }

        // POST: tblDepartamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDepartamentos tblDepartamentos = db.tblDepartamentos.Find(id);
            db.tblDepartamentos.Remove(tblDepartamentos);
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
