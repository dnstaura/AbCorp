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
    public class tblCategoriasController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblCategorias
        public ActionResult Index()
        {
            return View(db.tblCategorias.ToList());
        }

        // GET: tblCategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategorias tblCategorias = db.tblCategorias.Find(id);
            if (tblCategorias == null)
            {
                return HttpNotFound();
            }
            return View(tblCategorias);
        }

        // GET: tblCategorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Categoria,Nombre_Categoria")] tblCategorias tblCategorias)
        {
            if (ModelState.IsValid)
            {
                db.tblCategorias.Add(tblCategorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCategorias);
        }

        // GET: tblCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategorias tblCategorias = db.tblCategorias.Find(id);
            if (tblCategorias == null)
            {
                return HttpNotFound();
            }
            return View(tblCategorias);
        }

        // POST: tblCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Categoria,Nombre_Categoria")] tblCategorias tblCategorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCategorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCategorias);
        }

        // GET: tblCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategorias tblCategorias = db.tblCategorias.Find(id);
            if (tblCategorias == null)
            {
                return HttpNotFound();
            }
            return View(tblCategorias);
        }

        // POST: tblCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCategorias tblCategorias = db.tblCategorias.Find(id);
            db.tblCategorias.Remove(tblCategorias);
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
