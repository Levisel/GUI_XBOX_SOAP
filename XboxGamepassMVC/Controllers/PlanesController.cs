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
    public class GUIPlanesController : Controller
    {
        LogicaPlanes planesDB = new LogicaPlanes();
        private IAppCache cache = new CachingService();

        // GET: Planes
        public ActionResult Index(string searchNombre, string searchPlataforma, int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);

            var planes = cache.GetOrAdd("planesKey", () => planesDB.ListarPlanes(), DateTimeOffset.Now.AddHours(1));

            if (!string.IsNullOrEmpty(searchNombre))
            {
                planes = planes.Where(p => p.Plan_Nombre.Contains(searchNombre)).ToList();
            }

            if (!string.IsNullOrEmpty(searchPlataforma))
            {
                planes = planes.Where(p => p.Plan_Plataforma.Equals(searchPlataforma)).ToList();
            }

            ViewBag.Plan_Plataforma = new SelectList(new[] { "Xbox", "PC", "Xbox y PC" });
            var lstPlanes = planes.ToPagedList(pageNumber, pageSize);

            return View(lstPlanes);
        }

        // GET: Planes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPlan modelPlan = planesDB.buscarPlan(id.Value);
            if (modelPlan == null)
            {
                return HttpNotFound();
            }
            return View(modelPlan);
        }

        // GET: Planes/Create
        public ActionResult Create()
        {
            ViewBag.Plan_Duracion = new SelectList(new[] { "1 Mes", "3 Meses", "12 Meses" });
            ViewBag.Plan_Plataforma = new SelectList(new[] { "Xbox", "PC", "Xbox y PC" });
            return View();
        }

        // POST: Planes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Plan_ID,Plan_Nombre,Plan_Duracion,Plan_Precio,Plan_Plataforma,Plan_LogicalDelete")] ModelPlan modelPlan)
        {
            if (ModelState.IsValid)
            {
                planesDB.crearPlan(modelPlan);
                cache.Remove("planesKey"); // Invalida la caché
                return RedirectToAction("Index");
            }
            ViewBag.Plan_Duracion = new SelectList(new[] { "1 Mes", "3 Meses", "12 Meses" });
            ViewBag.Plan_Plataforma = new SelectList(new[] { "Xbox", "PC", "Xbox y PC" });
            return View(modelPlan);
        }

        // GET: Planes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPlan modelPlan = planesDB.buscarPlan(id.Value);
            if (modelPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.Plan_Duracion = new SelectList(new[] { "1 Mes", "3 Meses", "12 Meses" }, modelPlan.Plan_Duracion);
            ViewBag.Plan_Plataforma = new SelectList(new[] { "Xbox", "PC", "Xbox y PC" }, modelPlan.Plan_Plataforma);
            return View(modelPlan);
        }

        // POST: Planes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Plan_ID,Plan_Nombre,Plan_Duracion,Plan_Precio,Plan_Plataforma,Plan_LogicalDelete")] ModelPlan modelPlan)
        {
            if (ModelState.IsValid)
            {
                planesDB.actualizarPlan(modelPlan);
                cache.Remove("planesKey"); // Invalida la caché
                return RedirectToAction("Index");
            }
            ViewBag.Plan_Duracion = new SelectList(new[] { "1 Mes", "3 Meses", "12 Meses" }, modelPlan.Plan_Duracion);
            ViewBag.Plan_Plataforma = new SelectList(new[] { "Xbox", "PC", "Xbox y PC" }, modelPlan.Plan_Plataforma);
            return View(modelPlan);
        }

        // GET: Planes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPlan modelPlan = planesDB.buscarPlan(id.Value);
            if (modelPlan == null)
            {
                return HttpNotFound();
            }
            return View(modelPlan);
        }

        // POST: Planes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            planesDB.eliminarPlan(id);
            cache.Remove("planesKey"); // Invalida la caché
            return RedirectToAction("Index");
        }

        // Método para obtener los productos en formato JSON
        [HttpGet]
        public JsonResult ObtenerProductos()
        {
            var productosDto = planesDB.ListarPlanes().Select(p => new
            {
                prd_Codigo = p.Plan_ID,
                prd_Nombre = p.Plan_Nombre,
                prd_Duracion = p.Plan_Duracion,
                prd_Precio = p.Plan_Precio
            });

            return Json(productosDto, JsonRequestBehavior.AllowGet);
        }
    }
}
