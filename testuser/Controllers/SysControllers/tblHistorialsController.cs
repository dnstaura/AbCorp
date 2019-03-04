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
    public class tblHistorialsController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblHistorials
        public ActionResult Index(int? id)
        {
            /*Retorna lista de historiales*/
            var tblHistorial = db.tblHistorial.Include(t => t.tblCasos);

            /*retorna lista de casos en historial*/
            var casoshistorial = db.tblHistorial.Where(x => x.Id_Caso == id).ToList();

            if (casoshistorial.Count > 0)
            {
            ViewBag.casehistory = casoshistorial;
            }
            else
            {

            }

            return View(tblHistorial.ToList());
        }


        // GET: tblHistorials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            if (tblHistorial == null)
            {
                return HttpNotFound();
            }
            return View(tblHistorial);
        }

        // GET: tblHistorials/Create
        public ActionResult Create()
        {
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso");
            return View();
        }

        // POST: tblHistorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Historial,Id_Caso,Fecha_Inicio,Fecha_Final,Descripcion,Archivo,Fecha_Agregado")] tblHistorial tblHistorial)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblHistorial.Archivo = "/Content/Historial/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Historial/") + img);

                    }
                    db.tblHistorial.Add(tblHistorial);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
              
            }

            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblHistorial.Id_Caso);
            return View(tblHistorial);
        }

        // GET: tblHistorials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            if (tblHistorial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblHistorial.Id_Caso);
            return View(tblHistorial);
        }

        // POST: tblHistorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Historial,Id_Caso,Fecha_Inicio,Fecha_Final,Descripcion,Archivo,Fecha_Agregado")] tblHistorial tblHistorial)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblHistorial.Archivo = "/Content/Historial/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Historial/") + img);

                    }

                }
                db.Entry(tblHistorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Caso = new SelectList(db.tblCasos, "Id_Caso", "Numero_Caso", tblHistorial.Id_Caso);
            return View(tblHistorial);
        }

        // GET: tblHistorials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            if (tblHistorial == null)
            {
                return HttpNotFound();
            }
            return View(tblHistorial);
        }

        // POST: tblHistorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblHistorial tblHistorial = db.tblHistorial.Find(id);
            db.tblHistorial.Remove(tblHistorial);
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
