using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testuser.Models.SysModel;

namespace testuser.Controllers.SysControllers
{
    public class tblAfavordeController : Controller
    {
        dbModel db = new dbModel();

        // GET: tblAfavorde
        [Authorize]
        public ActionResult Index()
        {
            var dato = db.tblAfavorde.Where(x => x.idfavorde == x.idfavorde).Count();
            ViewBag.af = dato;

            return View(db.tblAfavorde.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblAfavorde af)
        {
            if (ModelState.IsValid)
            {
                db.tblAfavorde.Add(af);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View();
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var datos = db.tblAfavorde.Find(id);

            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View(datos);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblAfavorde af)
        {
                ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
                ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");

            if (ModelState.IsValid)
            {

                db.Entry(af).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            var datos = db.tblAfavorde.Find(id);
            return View(datos);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var datos = db.tblAfavorde.Find(id);
            ViewBag.Id_Departamento = new SelectList(db.tblDepartamentos, "Id_Departamento", "Departamento");
            ViewBag.Id_Municipio = new SelectList(db.tblMunicipios, "id_Municipio", "Municipio");
            return View(datos);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(tblAfavorde af, int id)
        {
            if (ModelState.IsValid)
            {
                var datos = db.tblAfavorde.Find(id);
                db.tblAfavorde.Remove(datos);
                db.SaveChanges();
                return RedirectToAction("Index");
            } return View();
        }
    }
}