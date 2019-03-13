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
    public class tblClientesController : Controller
    {
        private dbModel db = new dbModel();

        // GET: tblClientes
        [Authorize]
        public ActionResult Index()
        {
            var tblClientes = db.tblClientes.Include(t => t.tblEstadosRegistros);

            /*Contador clientes*/
            var cont = db.tblClientes.Where(x => x.Id_Cliente == x.Id_Cliente).Count();
            ViewBag.Cliente = cont;
            /**/
            return View(tblClientes.ToList());
        }

        // GET: tblClientes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClientes tblClientes = db.tblClientes.Find(id);
            if (tblClientes == null)
            {
                return HttpNotFound();
            }
            return View(tblClientes);
        }

        // GET: tblClientes/Create
        [Authorize]
        public ActionResult Create()
        {
            List<SelectListItem> genero = new List<SelectListItem>();
            genero.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
            genero.Add(new SelectListItem() { Text = "Femenino", Value = "Femenino" });
            ViewBag.gen = genero;

            ViewData["FechaActual"] = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado");
            return View();
        }

        // POST: tblClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cliente,Nombres,Apellidos,Genero,Fecha_Nacimiento,Telefono,Direccion,Correo,Id_EstadoRegistro,Fecha_Agregado")] tblClientes tblClientes)
        {
            if (ModelState.IsValid)
            {
                db.tblClientes.Add(tblClientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado", tblClientes.Id_EstadoRegistro);
            return View(tblClientes);
        }

        // GET: tblClientes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClientes tblClientes = db.tblClientes.Find(id);
            if (tblClientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado", tblClientes.Id_EstadoRegistro);
            return View(tblClientes);
        }

        // POST: tblClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cliente,Nombres,Apellidos,Genero,Fecha_Nacimiento,Telefono,Direccion,Correo,Id_EstadoRegistro,Fecha_Agregado")] tblClientes tblClientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblClientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_EstadoRegistro = new SelectList(db.tblEstadosRegistros, "Id_EstadoRegistro", "Estado", tblClientes.Id_EstadoRegistro);
            return View(tblClientes);
        }

        // GET: tblClientes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClientes tblClientes = db.tblClientes.Find(id);
            if (tblClientes == null)
            {
                return HttpNotFound();
            }
            return View(tblClientes);
        }

        // POST: tblClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblClientes tblClientes = db.tblClientes.Find(id);
            db.tblClientes.Remove(tblClientes);
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
