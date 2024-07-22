using PagedList;
using PagedList.Mvc;
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
using LazyCache;
namespace XboxGamepassMVC.Controllers
{
    public class GUIClientesController : Controller
    {

        LogicaClientes clientesDB = new LogicaClientes();

        // GET: ModelClientes
        private IAppCache cache = new CachingService();

        public ActionResult Index(string searchID, string searchNombre, string searchApellido, int? page)
        {
            var clientes = cache.GetOrAdd("clientesKey", () => clientesDB.ListarClientes(), DateTimeOffset.Now.AddHours(1));
            int pageSize = 100;
            int pageNumber = (page ?? 1);

            if (!string.IsNullOrEmpty(searchID))
            {
                clientes = clientes.Where(c => c.Cli_Cedula.Contains(searchID)).ToList();
            }

            if (!string.IsNullOrEmpty(searchNombre))
            {
                clientes = clientes.Where(c => c.Cli_Nombre.Contains(searchNombre)).ToList();
            }

            if (!string.IsNullOrEmpty(searchApellido))
            {
                clientes = clientes.Where(c => c.Cli_Apellido.Contains(searchApellido)).ToList();
            }
            var lstClientes = clientes.ToPagedList(pageNumber, pageSize);

            return View(lstClientes);
        }

        // GET: ModelClientes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelCliente modelCliente = clientesDB.buscarCliente(id);
            if (modelCliente == null)
            {
                return HttpNotFound();
            }
            return View(modelCliente);
        }

        // GET: ModelClientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModelClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cli_Cedula,Cli_Nombre,Cli_Apellido,Cli_BirthDate,Cli_Pais,Cli_Email,Cli_LogicalDelete")] ModelCliente modelCliente)
        {
            if (ModelState.IsValid)
            {
                clientesDB.crearCliente(modelCliente);
                cache.Remove("clientesKey"); // Invalida la caché
                return RedirectToAction("Index");
            }

            return View(modelCliente);
        }

        // GET: ModelClientes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelCliente modelCliente = clientesDB.buscarCliente(id);
            if (modelCliente == null)
            {
                return HttpNotFound();
            }
            return View(modelCliente);
        }

        // POST: ModelClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cli_Cedula,Cli_Nombre,Cli_Apellido,Cli_BirthDate,Cli_Pais,Cli_Email,Cli_LogicalDelete")] ModelCliente modelCliente)
        {
            if (ModelState.IsValid)
            {
                clientesDB.actualizarCliente(modelCliente);
                cache.Remove("clientesKey"); // Invalida la caché
                return RedirectToAction("Index");
            }
            return View(modelCliente);
        }

        // GET: ModelClientes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelCliente modelCliente = clientesDB.buscarCliente(id);
            if (modelCliente == null)
            {
                return HttpNotFound();
            }
            return View(modelCliente);
        }

        // POST: ModelClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            clientesDB.eliminarCliente(id);
            cache.Remove("clientesKey"); // Invalida la caché
            return RedirectToAction("Index");
        }

        // En tu controlador
        [HttpGet]
        public JsonResult BuscarCliente(string id)
        {
            var cliente = clientesDB.ListarClientes().Where(c => c.Cli_Cedula == id).FirstOrDefault();
            return Json(cliente, JsonRequestBehavior.AllowGet);
        }






    }
}
