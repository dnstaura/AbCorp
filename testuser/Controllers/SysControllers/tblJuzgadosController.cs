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
    public class tblJuzgadosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblJuzgados
        [Authorize(Roles = "Administrador,Abogado,Observador")]
        public ActionResult Index()
        {
            var tblJuzgados = db.tblJuzgados.Include(t => t.tblDepartamentos).Include(t => t.tblMunicipios);
            return View(tblJuzgados.ToList());
        }

        // GET: tblJuzgados/Details/5
        [Authorize(Roles = "Administrador,Abogado,Observador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblJuzgados tblJuzgados = db.tblJuzgados.Find(id);
            if (tblJuzgados == null)
            {
                return HttpNotFound();
            }
            return View(tblJuzgados);
        }

        // GET: tblJuzgados/Create
        [Authorize(Roles = "Administrador,Abogado")]
        public ActionResult Create()
        {
            List<tblDepartamentos> DepartamentosList = db.tblDepartamentos.ToList();
            ViewBag.DepartamentosList = new SelectList(DepartamentosList, "Id_Departamento", "Departamento");
            //ViewBag.tblDepartamentos = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View();
        }

        // POST: tblJuzgados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Juzgado,Juzgado,telefono,correo,direccion,Id_Departamento,Id_Municipio")] tblJuzgados tblJuzgados)
        {
            if (ModelState.IsValid)
            {
                db.tblJuzgados.Add(tblJuzgados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<tblDepartamentos> DepartamentosList = db.tblDepartamentos.ToList();
            ViewBag.DepartamentosList = new SelectList(DepartamentosList, "Id_Departamento", "Departamento", tblJuzgados.Id_Departamento);
            //ViewBag.tblDepartamentos = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblJuzgados.Id_Departamento);
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio", tblJuzgados.Id_Municipio);
            return View(tblJuzgados);
        }

        // GET: tblJuzgados/Edit/5
        [Authorize(Roles = "Administrador,Abogado")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblJuzgados tblJuzgados = db.tblJuzgados.Find(id);
            if (tblJuzgados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblJuzgados.Id_Departamento);
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio", tblJuzgados.Id_Municipio);
            return View(tblJuzgados);
        }

        // POST: tblJuzgados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Abogado")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Juzgado,Juzgado,telefono,correo,direccion,Id_Departamento,Id_Municipio")] tblJuzgados tblJuzgados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblJuzgados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblJuzgados.Id_Departamento);
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio", tblJuzgados.Id_Municipio);
            return View(tblJuzgados);
        }

        // GET: tblJuzgados/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblJuzgados tblJuzgados = db.tblJuzgados.Find(id);
            if (tblJuzgados == null)
            {
                return HttpNotFound();
            }
            return View(tblJuzgados);
        }

        // POST: tblJuzgados/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblJuzgados tblJuzgados = db.tblJuzgados.Find(id);
            db.tblJuzgados.Remove(tblJuzgados);
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

        public JsonResult GetMunicipiosLIst(int Id_Departamento)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tblMunicipios> ListaMunicipios = db.tblMunicipios.Where(x => x.Id_Departamento == Id_Departamento).ToList();
            return Json(ListaMunicipios, JsonRequestBehavior.AllowGet);
        }
    }
}
