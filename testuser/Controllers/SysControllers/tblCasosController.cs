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
    public class tblCasosController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblCasos
        public ActionResult Index()
        {
            var tblCasos = db.tblCasos.Include(t => t.tblCategorias).Include(t => t.tblClientes).Include(t => t.tblDepartamentos).Include(t => t.tblEstadosCasos).Include(t => t.tblJuzgados).Include(t => t.tblMunicipios);
            return View(tblCasos.ToList());
        }

        // GET: tblCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCasos tblCasos = db.tblCasos.Find(id);
            if (tblCasos == null)
            {
                return HttpNotFound();
            }
            return View(tblCasos);
        }

        // GET: tblCasos/Create
        public ActionResult Create()
        {
            ViewData["Fecha_Agregado"] = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Id_Categoria = new SelectList(db.tblCategorias, "Id_Categoria", "Nombre_Categoria");
            ViewBag.Id_Cliente = new SelectList(db.tblClientes, "Id_Cliente", "Nombres");
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_EstadoCaso = new SelectList(db.tblEstadosCasos, "Id_EstadoCaso", "Nombre_Estado");
            ViewBag.Id_Juzgado = new SelectList(db.tblJuzgados, "Id_Juzgado", "Juzgado");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View();
        }

        // POST: tblCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Caso,Id_Cliente,Id_Categoria,Id_EstadoCaso,Id_Departamento,Id_Municipio,Id_Juzgado,Titulo,Numero_Caso,Fecha_Inicio,Fecha_Audiencia,Fecha_Agregado")] tblCasos tblCasos)
        {
            if (ModelState.IsValid)
            {
                ViewData["Fecha_Agregado"] = DateTime.Now.ToString();
                db.tblCasos.Add(tblCasos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Categoria = new SelectList(db.tblCategorias, "Id_Categoria", "Nombre_Categoria", tblCasos.Id_Categoria);
            ViewBag.Id_Cliente = new SelectList(db.tblClientes, "Id_Cliente", "Nombres", tblCasos.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblCasos.Id_Departamento);
            ViewBag.Id_EstadoCaso = new SelectList(db.tblEstadosCasos, "Id_EstadoCaso", "Nombre_Estado", tblCasos.Id_EstadoCaso);
            ViewBag.Id_Juzgado = new SelectList(db.tblJuzgados, "Id_Juzgado", "Juzgado", tblCasos.Id_Juzgado);
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio", tblCasos.Id_Municipio);
            return View(tblCasos);
        }

        // GET: tblCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCasos tblCasos = db.tblCasos.Find(id);
            if (tblCasos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Categoria = new SelectList(db.tblCategorias, "Id_Categoria", "Nombre_Categoria", tblCasos.Id_Categoria);
            ViewBag.Id_Cliente = new SelectList(db.tblClientes, "Id_Cliente", "Nombres", tblCasos.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblCasos.Id_Departamento);
            ViewBag.Id_EstadoCaso = new SelectList(db.tblEstadosCasos, "Id_EstadoCaso", "Nombre_Estado", tblCasos.Id_EstadoCaso);
            ViewBag.Id_Juzgado = new SelectList(db.tblJuzgados, "Id_Juzgado", "Juzgado", tblCasos.Id_Juzgado);
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio", tblCasos.Id_Municipio);
            return View(tblCasos);
        }

        // POST: tblCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Caso,Id_Cliente,Id_Categoria,Id_EstadoCaso,Id_Departamento,Id_Municipio,Id_Juzgado,Titulo,Numero_Caso,Fecha_Inicio,Fecha_Audiencia,Fecha_Agregado")] tblCasos tblCasos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCasos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Categoria = new SelectList(db.tblCategorias, "Id_Categoria", "Nombre_Categoria", tblCasos.Id_Categoria);
            ViewBag.Id_Cliente = new SelectList(db.tblClientes, "Id_Cliente", "Nombres", tblCasos.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento", tblCasos.Id_Departamento);
            ViewBag.Id_EstadoCaso = new SelectList(db.tblEstadosCasos, "Id_EstadoCaso", "Nombre_Estado", tblCasos.Id_EstadoCaso);
            ViewBag.Id_Juzgado = new SelectList(db.tblJuzgados, "Id_Juzgado", "Juzgado", tblCasos.Id_Juzgado);
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio", tblCasos.Id_Municipio);
            return View(tblCasos);
        }

        // GET: tblCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCasos tblCasos = db.tblCasos.Find(id);
            if (tblCasos == null)
            {
                return HttpNotFound();
            }
            return View(tblCasos);
        }

        // POST: tblCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCasos tblCasos = db.tblCasos.Find(id);
            db.tblCasos.Remove(tblCasos);
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
