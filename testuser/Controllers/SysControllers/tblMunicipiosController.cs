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
    public class tblMunicipiosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblMunicipios
        public ActionResult Index()
        {
            var tblMunicipios = db.tblMunicipios.Include(t => t.tblDepartamentos);
            return View(tblMunicipios.ToList());
        }

        // GET: tblMunicipios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMunicipios tblMunicipios = db.tblMunicipios.Find(id);
            if (tblMunicipios == null)
            {
                return HttpNotFound();
            }
            return View(tblMunicipios);
        }

        // GET: tblMunicipios/Create
        public ActionResult Create()
        {
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            return View();
        }

        // POST: tblMunicipios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Municipio,Municipio,Id_Departamento")] tblMunicipios tblMunicipios)
        {
            if (ModelState.IsValid)
            {
                db.tblMunicipios.Add(tblMunicipios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblMunicipios.Id_Departamento);
            return View(tblMunicipios);
        }

        // GET: tblMunicipios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMunicipios tblMunicipios = db.tblMunicipios.Find(id);
            if (tblMunicipios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblMunicipios.Id_Departamento);
            return View(tblMunicipios);
        }

        // POST: tblMunicipios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Municipio,Municipio,Id_Departamento")] tblMunicipios tblMunicipios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMunicipios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblMunicipios.Id_Departamento);
            return View(tblMunicipios);
        }

        // GET: tblMunicipios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMunicipios tblMunicipios = db.tblMunicipios.Find(id);
            if (tblMunicipios == null)
            {
                return HttpNotFound();
            }
            return View(tblMunicipios);
        }

        // POST: tblMunicipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMunicipios tblMunicipios = db.tblMunicipios.Find(id);
            db.tblMunicipios.Remove(tblMunicipios);
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
