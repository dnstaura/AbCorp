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
    public class tblEditablesController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblEditables
        public ActionResult Index()
        {
            return View(db.tblEditables.ToList());
        }

        // GET: tblEditables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEditables tblEditables = db.tblEditables.Find(id);
            if (tblEditables == null)
            {
                return HttpNotFound();
            }
            return View(tblEditables);
        }

        // GET: tblEditables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEditables/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEditable,fecha,entidad,instrumento,documento,tipo_documento,img,descripcion")] tblEditables tblEditables)
        {
            if (ModelState.IsValid)
            {
                db.tblEditables.Add(tblEditables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEditables);
        }

        // GET: tblEditables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEditables tblEditables = db.tblEditables.Find(id);
            if (tblEditables == null)
            {
                return HttpNotFound();
            }
            return View(tblEditables);
        }

        // POST: tblEditables/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEditable,fecha,entidad,instrumento,documento,tipo_documento,img,descripcion")] tblEditables tblEditables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEditables).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEditables);
        }

        // GET: tblEditables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEditables tblEditables = db.tblEditables.Find(id);
            if (tblEditables == null)
            {
                return HttpNotFound();
            }
            return View(tblEditables);
        }

        // POST: tblEditables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEditables tblEditables = db.tblEditables.Find(id);
            db.tblEditables.Remove(tblEditables);
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
