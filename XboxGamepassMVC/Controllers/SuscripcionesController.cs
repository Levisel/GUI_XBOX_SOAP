using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XboxGamepassMVC.Data;
using XboxGamepassMVC.Logica;
using XboxGamepassMVC.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Caching;
using LazyCache;
namespace XboxGamepassMVC.Controllers
{
    public class GUISuscripcionesController : Controller
    {
        LogicaSuscripciones suscripcionesDB = new LogicaSuscripciones();
        LogicaPlanes planesDB = new LogicaPlanes();
        private IAppCache cache = new CachingService();

        // GET: Suscripciones
        public ActionResult Index(string searchID, string searchNombre, DateTime? searchDate, string searchEstado, int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);

            var suscripciones = cache.GetOrAdd("suscripcionesKey", () => suscripcionesDB.ListarSuscripciones(), DateTimeOffset.Now.AddHours(1));

            if (!string.IsNullOrEmpty(searchID))
            {
                suscripciones = suscripciones.Where(s => s.Sus_ID.Contains(searchID)).ToList();
            }

            if (!string.IsNullOrEmpty(searchNombre))
            {
                suscripciones = suscripciones.Where(s => s.Plan_Nombre.Contains(searchNombre)).ToList();
            }

            if (searchDate.HasValue)
            {
                suscripciones = suscripciones.Where(s => s.Sus_StartDate.HasValue && s.Sus_StartDate.Value.Date == searchDate.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(searchEstado))
            {
                suscripciones = suscripciones.Where(s => s.Sus_Estado.Contains(searchEstado)).ToList();
            }

            ViewBag.Sus_Estado = new SelectList(new[] { "Activo", "Inactivo" });
            var lstSuscrip = suscripciones.ToPagedList(pageNumber, pageSize);

            return View(lstSuscrip);
        }

        // GET: Suscripciones/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelSuscripcion modelSuscripcion = suscripcionesDB.buscarSuscripcion(id);
            if (modelSuscripcion == null)
            {
                return HttpNotFound();
            }
            return View(modelSuscripcion);
        }

        // GET: Suscripciones/Create
        public ActionResult Create()
        {
            ViewBag.Plan_ID = new SelectList(suscripcionesDB.ObtenerPlanesConId(), "Key", "Value");
            ViewBag.Sus_Estado = new SelectList(new[] { "Activo", "Inactivo"});
            return View();
        }

        // POST: Suscripciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sus_ID,Plan_ID,Sus_StartDate,Sus_EndDate,Sus_Estado,Sus_RenovacionAuto,Sus_LogicalDelete")] ModelSuscripcion modelSuscripcion)
        {
            if (ModelState.IsValid)
            {
                suscripcionesDB.crearSuscripcion(modelSuscripcion);
                cache.Remove("suscripcionesKey"); // Invalida la caché
                return RedirectToAction("Index");
            }

            ViewBag.Plan_ID = new SelectList(suscripcionesDB.ObtenerPlanesConId(), "Key", "Value");
            ViewBag.Sus_Estado = new SelectList(new[] { "Activo", "Inactivo" });
            return View(modelSuscripcion);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelSuscripcion modelSuscripcion = suscripcionesDB.buscarSuscripcion(id);
            if (modelSuscripcion == null)
            {
                return HttpNotFound();
            }

            ViewBag.Plan_ID = new SelectList(suscripcionesDB.ObtenerPlanesConId(), "Key", "Value", modelSuscripcion.Plan_ID);
            ViewBag.Sus_Estado = new SelectList(new[] { "Activo", "Inactivo" }, modelSuscripcion.Sus_Estado);
            return View(modelSuscripcion);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sus_ID,Plan_ID,Sus_StartDate,Sus_EndDate,Sus_Estado,Sus_RenovacionAuto,Sus_LogicalDelete")] ModelSuscripcion modelSuscripcion)
        {
            if (ModelState.IsValid)
            {
                suscripcionesDB.actualizarSuscripcion(modelSuscripcion);
                cache.Remove("suscripcionesKey"); // Invalida la caché
                return RedirectToAction("Index");
            }
            ViewBag.Plan_ID = new SelectList(suscripcionesDB.ObtenerPlanesConId(), "Key", "Value", modelSuscripcion.Plan_ID);
            ViewBag.Sus_Estado = new SelectList(new[] { "Activo", "Inactivo" }, modelSuscripcion.Sus_Estado);
            return View(modelSuscripcion);
        }


        // GET: Suscripciones/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelSuscripcion modelSuscripcion = suscripcionesDB.buscarSuscripcion(id);
            if (modelSuscripcion == null)
            {
                return HttpNotFound();
            }
            return View(modelSuscripcion);
        }

        // POST: Suscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            suscripcionesDB.eliminarSuscripcion(id);
            cache.Remove("suscripcionesKey"); // Invalida la caché
            return RedirectToAction("Index");
        }
    }
}
