﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testuser.Models.SysModel;

namespace testuser.Controllers.SysControllers
{
    public class MainCalendarController : Controller
    {
        dbModel db = new dbModel();
        // GET: MainEventCalendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Indexx()
        {

            var dato = db.tblEventCalendar;

            return Json(dato.AsEnumerable().Select(a => new {
                id_Event = a.id_Event,
                title = a.title,
                descripcion = a.descripcion,
                color = a.color,
                textColor = a.textColor,
                start = a.start.ToUniversalTime().ToString("yyyy.MM.dd HH:mm:ss.fff"),
                end = a.end.ToUniversalTime().ToString("yyyy.MM.dd HH:mm:ss.fff")
            }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Indexx(int txtID, string btnAgregar, string btnEditar, string btnEliminar, string txtFechaInicio, string txtFechaFin, string txtTitulo, string txtHora, string txtDescripcion, string txtColor, tblEventCalendar ev)
        {
            if (Request.HttpMethod == "POST")
            {
                if (btnAgregar == "agregar")
                {
                    var datosevento = new tblEventCalendar
                    {
                        id_Event = txtID,
                        title = txtTitulo,
                        descripcion = txtDescripcion,
                        color = txtColor,
                        textColor = "#FFFFFF",
                        start = Convert.ToDateTime(txtFechaInicio + " " + txtHora),
                        end = Convert.ToDateTime(txtFechaFin + " " + txtHora)

                    };
                    db.tblEventCalendar.Add(datosevento);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else if (btnEditar == "editar")
                {

                    var datosevento = new tblEventCalendar
                    {
                        id_Event = txtID,
                        title = txtTitulo,
                        descripcion = txtDescripcion,
                        color = txtColor,
                        textColor = "#FFFFFF",
                        start = Convert.ToDateTime(txtFechaInicio),
                        end = Convert.ToDateTime(txtFechaFin)
                    };
                    db.tblEventCalendar.Attach(datosevento);
                    db.Entry(datosevento).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else if (btnEliminar == "eliminar")
                {
                    var datosevento = new tblEventCalendar
                    {
                        id_Event = txtID,
                        title = txtTitulo,
                        descripcion = txtDescripcion,
                        color = txtColor,
                        textColor = "#FFFFFF",
                        start = Convert.ToDateTime(txtFechaInicio),
                        end = Convert.ToDateTime(txtFechaFin)
                    };
                    db.tblEventCalendar.Attach(datosevento);
                    db.tblEventCalendar.Remove(datosevento);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Index", "Home");



        }

    }
}