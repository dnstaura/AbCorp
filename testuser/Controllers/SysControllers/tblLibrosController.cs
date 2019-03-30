using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using testuser.Models.SysModel;
using System.Data.Entity.SqlServer;
using testuser.Models;
using Microsoft.AspNet.Identity;

namespace testuser.Controllers.SysControllers
{
    public class tblLibrosController : Controller
    {

        private dbModel db = new dbModel();

        // GET: tblLibros
        [Authorize(Roles ="Administrador,Notario,Observador")]
        public ActionResult Index()
        {
            var tblLibros = db.tblLibros.Include(t => t.tblPersonal);

            //Contador Libros
            var libcount = (from a in db.tblLibros
                           where a.idlibros == a.idlibros
                           select a).Count();

            ViewBag.lb = libcount;

            return View(tblLibros.ToList());
        }

        // GET: tblLibros/Details/5
        [Authorize(Roles = "Administrador,Notario,Observador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLibros tblLibros = db.tblLibros.Find(id);
            if (tblLibros == null)
            {
                return HttpNotFound();
            }
            return View(tblLibros);
        }

        // GET: tblLibros/Create
        [Authorize(Roles = "Administrador,Notario")]
        public ActionResult Create()
        {
            //ViewBag.idfavorde = new SelectList(db.tblAfavorde, "idfavorde", "nombres");
            //ViewBag.idotorgante = new SelectList(db.tblOtorgante, "idotorgante", "nombres");
            ViewBag.idfavorde = new SelectList(db.tblAfavorde.Join(
                                db.tblLibros,
                                a => a.idfavorde,
                                b => b.idlibros,
                                (v, s) =>
                                 new
                                 {
                                     v = v,
                                     s = s
                                 }
                           )
                           .Select(
                            temp0 =>
                            new
                            {
                                v = temp0.v,
                                s = temp0.s
                            }
                            ).Select(m => new SelectListItem
                            {
                                Value = SqlFunctions.StringConvert((double)m.s.idlibros).Trim(),
                                Text = m.v.nombres + " " + m.v.apellidos
                            }), "Value", "Text", 0);

            ViewBag.idotorgante = new SelectList(db.tblOtorgante.Join(
                                db.tblLibros,
                                a => a.idotorgante,
                                b => b.idlibros,
                                (v, s) =>
                                 new
                                 {
                                     v = v,
                                     s = s
                                 }
                           )
                           .Select(
                            temp0 =>
                            new
                            {
                                v = temp0.v,
                                s = temp0.s
                            }
                            ).Select(m => new SelectListItem
                            {
                                Value = SqlFunctions.StringConvert((double)m.s.idlibros).Trim(),
                                Text = m.v.nombres + " " + m.v.apellidos
                            }), "Value", "Text", 0);


            ViewBag.personalNotario = (from p in db.tblPersonal
                            join c in db.tblCategoriaPersonal on p.id_CategoriaPersonal equals c.id_CategoriaPersonal
                            where c.CategoriaPersonal == "Notario"
                            //where c.CategoriaPersonal == "Notario"
                            select p).ToList();

            //ViewBag.id_Personal = new SelectList(notarios, "Id_Personal", "Nombres", "Apellidos");
            return View();
        }

        // POST: tblLibros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idlibros,fecha,correlativo,instrumento,folios,libro,img,word,idotorgante,idfavorde,id_Personal")] tblLibros tblLibros)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    HttpPostedFileBase file2 = Request.Files[1];

                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblLibros.img = "/Content/Libros/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Libros/") + img);

                    }
                    if (file2.ContentLength > 0)
                    {
                        var doc = (file2.FileName).ToLower();
                        tblLibros.word = "/Content/Libros/" + doc;
                        file2.SaveAs(Server.MapPath("~/Content/Libros/") + doc);
                    }
                    db.tblLibros.Add(tblLibros);
                    db.SaveChanges();

                    /*NOTIFICACION*/
                    ApplicationDbContext dbs = new ApplicationDbContext();
                    Notifications notificacion = new Notifications();
                    notificacion.Module = "Libros";
                    notificacion.Message = string.Format("Registro un nuevo libro");
                    notificacion.Date = DateTime.Now;
                    notificacion.Viewed = false;
                    notificacion.Usuario_Id = User.Identity.GetUserId();

                    dbs.Notification.Add(notificacion);
                    dbs.SaveChanges();
                    /*FIN NOTIFICACION*/

                    return RedirectToAction("Index");
                }
            }

            ViewBag.idfavorde = new SelectList(db.tblAfavorde, "idfavorde", "nombres", tblLibros.idfavorde);
            ViewBag.idotorgante = new SelectList(db.tblOtorgante, "idotorgante", "nombres", tblLibros.idotorgante);
            ViewBag.id_Personal = new SelectList(db.tblPersonal, "Id_Personal", "Nombres", tblLibros.id_Personal);
         


            return View(tblLibros);
        }

        // GET: tblLibros/Edit/5
        [Authorize(Roles = "Administrador,Notario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLibros tblLibros = db.tblLibros.Find(id);
            if (tblLibros == null)
            {
                return HttpNotFound();
            }
            ViewBag.idfavorde = new SelectList(db.tblAfavorde.Join(
                                           db.tblLibros,
                                           a => a.idfavorde,
                                           b => b.idlibros,
                                           (v, s) =>
                                            new
                                            {
                                                v = v,
                                                s = s
                                            }
                                      )
                                      .Select(
                                       temp0 =>
                                       new
                                       {
                                           v = temp0.v,
                                           s = temp0.s
                                       }
                                       ).Select(m => new SelectListItem
                                       {
                                           Value = SqlFunctions.StringConvert((double)m.s.idlibros).Trim(),
                                           Text = m.v.nombres + " " + m.v.apellidos
                                       }), "Value", "Text", 0);

            ViewBag.idotorgante = new SelectList(db.tblOtorgante.Join(
                                db.tblLibros,
                                a => a.idotorgante,
                                b => b.idlibros,
                                (v, s) =>
                                 new
                                 {
                                     v = v,
                                     s = s
                                 }
                           )
                           .Select(
                            temp0 =>
                            new
                            {
                                v = temp0.v,
                                s = temp0.s
                            }
                            ).Select(m => new SelectListItem
                            {
                                Value = SqlFunctions.StringConvert((double)m.s.idlibros).Trim(),
                                Text = m.v.nombres + " " + m.v.apellidos
                            }), "Value", "Text", 0);


            ViewBag.personalNotario = (from p in db.tblPersonal
                                       join c in db.tblCategoriaPersonal on p.id_CategoriaPersonal equals c.id_CategoriaPersonal
                                       where c.CategoriaPersonal == "Notario"
                                       //where c.CategoriaPersonal == "Notario"
                                       select p).ToList();
            return View(tblLibros);
        }

        // POST: tblLibros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador,Notario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idlibros,fecha,correlativo,instrumento,folios,libro,img,word,idotorgante,idfavorde,id_Personal")] tblLibros tblLibros)
        {
            if (ModelState.IsValid)
            {
               
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    HttpPostedFileBase file2 = Request.Files[1];

                    if (file.ContentLength > 0)
                    {
                        var img = (file.FileName).ToLower();
                        tblLibros.img = "/Content/Libros/" + img;
                        file.SaveAs(Server.MapPath("~/Content/Libros/") + img);

                    }
                    if (file2.ContentLength > 0)
                    {
                        var doc = (file2.FileName).ToLower();
                        tblLibros.word = "/Content/Libros/" + doc;
                        file2.SaveAs(Server.MapPath("~/Content/Libros/") + doc);
                    }

                    var querypdf = (from x in db.tblLibros
                                 where x.idlibros == tblLibros.idlibros
                                 select x.img).First();

                    var queryword = (from x in db.tblLibros
                                    where x.idlibros == tblLibros.idlibros
                                    select x.word).First();

                    if (file.ContentLength == 0)
                    {
                        
                        tblLibros.img = querypdf;
                        //tblLibros.word = query.word;
                    }
                    if (file2.ContentLength == 0)
                    {
                        //var query = (from x in db.tblLibros
                        //             where x.idlibros == tblLibros.idlibros
                        //             select x.word).First();
                        //tblLibros.img = query.img;
                        tblLibros.word = queryword;
                    }

                    db.Entry(tblLibros).State= EntityState.Modified;
                    db.SaveChanges();
                }

                /*NOTIFICACION*/
                ApplicationDbContext dbs = new ApplicationDbContext();
                Notifications notificacion = new Notifications();
                notificacion.Module = "Libros";
                notificacion.Message = string.Format("Edito un libro");
                notificacion.Date = DateTime.Now;
                notificacion.Viewed = false;
                notificacion.Usuario_Id = User.Identity.GetUserId();

                dbs.Notification.Add(notificacion);
                dbs.SaveChanges();
                /*FIN NOTIFICACION*/

                return RedirectToAction("Index");
            }
            ViewBag.idfavorde = new SelectList(db.tblAfavorde, "idfavorde", "nombres", tblLibros.idfavorde);
            ViewBag.idotorgante = new SelectList(db.tblOtorgante, "idotorgante", "nombres", tblLibros.idotorgante);
            ViewBag.id_Personal = new SelectList(db.tblPersonal, "Id_Personal", "Nombres", tblLibros.id_Personal);
            return View(tblLibros);
        }

        // GET: tblLibros/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLibros tblLibros = db.tblLibros.Find(id);
            if (tblLibros == null)
            {
                return HttpNotFound();
            }
            return View(tblLibros);
        }

        // POST: tblLibros/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLibros tblLibros = db.tblLibros.Find(id);
            db.tblLibros.Remove(tblLibros);
            db.SaveChanges();

            /*NOTIFICACION*/
            ApplicationDbContext dbs = new ApplicationDbContext();
            Notifications notificacion = new Notifications();
            notificacion.Module = "Libros";
            notificacion.Message = string.Format("Elimino un libro");
            notificacion.Date = DateTime.Now;
            notificacion.Viewed = false;
            notificacion.Usuario_Id = User.Identity.GetUserId();

            dbs.Notification.Add(notificacion);
            dbs.SaveChanges();
            /*FIN NOTIFICACION*/

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
