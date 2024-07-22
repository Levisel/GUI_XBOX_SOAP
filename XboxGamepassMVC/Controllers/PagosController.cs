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
    public class GUIPagosController : Controller
    {
        LogicaPagos pagosDB = new LogicaPagos();
        private IAppCache cache = new CachingService();

        // GET: Pagos
        public ActionResult Index(string searchNombre, DateTime? searchDate, string searchEstado, int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);

            var pagos = cache.GetOrAdd("pagosKey", () => pagosDB.ListarPagos(), DateTimeOffset.Now.AddHours(1));

            if (!string.IsNullOrEmpty(searchNombre))
            {
                pagos = pagos.Where(p => p.Cliente_Nombre.Contains(searchNombre)).ToList();
            }

            if (searchDate.HasValue)
            {
                pagos = pagos.Where(p => p.Pago_Fecha.HasValue && p.Pago_Fecha.Value.Date == searchDate.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(searchEstado))
            {
                pagos = pagos.Where(p => p.Pago_Estado.Contains(searchEstado)).ToList();
            }

            ViewBag.Pago_Estado = new SelectList(new[] { "Pendiente", "Pagado" });
            var lstPagos = pagos.ToPagedList(pageNumber, pageSize);

            return View(lstPagos);
        }


        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPago modelPago = pagosDB.buscarPago(id.Value);
            if (modelPago == null)
            {
                return HttpNotFound();
            }
            return View(modelPago);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            ViewBag.Cli_Cedula = new SelectList(pagosDB.ObtenerClientesConId(), "Key", "Value");
            ViewBag.Sus_ID = new SelectList(pagosDB.ObtenerIDsDeSuscripcion());
            ViewBag.Pago_Estado = new SelectList(new[] { "Pendiente", "Pagado"});
            return View();
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pago_ID,Pago_Codigo,Cli_Cedula,Sus_ID,Pago_Monto,Pago_Fecha,Pago_Estado,Pago_LogicalDelete")] ModelPago modelPago)
        {
            if (ModelState.IsValid)
            {
                pagosDB.crearPago(modelPago);
                cache.Remove("pagosKey"); // Invalida la caché
                return RedirectToAction("Index");
            }

            ViewBag.Cli_Cedula = new SelectList(pagosDB.ObtenerClientesConId(), "Key", "Value");
            ViewBag.Sus_ID = new SelectList(pagosDB.ObtenerIDsDeSuscripcion());
            ViewBag.Pago_Estado = new SelectList(new[] { "Pendiente", "Pagado" });
            return View(modelPago);
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPago modelPago = pagosDB.buscarPago(id.Value);
            if (modelPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cli_Cedula = new SelectList(pagosDB.ObtenerClientesConId(), "Key", "Value", modelPago.Cli_Cedula);
            ViewBag.Sus_ID = new SelectList(pagosDB.ObtenerIDsDeSuscripcion(), modelPago.Sus_ID);
            ViewBag.Pago_Estado = new SelectList(new[] { "Pendiente", "Pagado" }, modelPago.Pago_Estado);
            return View(modelPago);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pago_ID,Pago_Codigo,Cli_Cedula,Sus_ID,Pago_Monto,Pago_Fecha,Pago_Estado,Pago_LogicalDelete")] ModelPago modelPago)
        {
            if (ModelState.IsValid)
            {
                pagosDB.actualizarPago(modelPago);
                cache.Remove("pagosKey"); // Invalida la caché
                return RedirectToAction("Index");
            }
            ViewBag.Cli_Cedula = new SelectList(pagosDB.ObtenerClientesConId(), "Key", "Value", modelPago.Cli_Cedula);
            ViewBag.Sus_ID = new SelectList(pagosDB.ObtenerIDsDeSuscripcion(), modelPago.Sus_ID);
            ViewBag.Pago_Estado = new SelectList(new[] { "Pendiente", "Pagado" }, modelPago.Pago_Estado);
            return View(modelPago);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPago modelPago = pagosDB.buscarPago(id.Value);
            if (modelPago == null)
            {
                return HttpNotFound();
            }
            return View(modelPago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pagosDB.eliminarPago(id);
            cache.Remove("pagosKey"); // Invalida la caché
            return RedirectToAction("Index");
        }
    }
}
