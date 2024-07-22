using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XboxGamepassMVC.Logica;
using XboxGamepassMVC.Models;
using PagedList;
using PagedList.Mvc;
namespace XboxGamepassMVC.Controllers
{
    public class SPController : Controller
    {
        // GET: SP
        LogicaSP logica = new LogicaSP();

        public ActionResult Analisis(int? page)
        {
            int pageSize = 100; // Cambia el tamaño de página según tu necesidad
            int pageNumber = (page ?? 1);

            var model = new ModelSPUnited
            {
                ClientesSuscripcionActivaInactiva = logica.lstSuscripcionActivaInactiva().ToPagedList(pageNumber, pageSize),
                ClientesPorPaisYPlan = logica.lstClientesPaisPlan().ToPagedList(pageNumber, pageSize),
                ClientesPorPlataforma = logica.lstClientesPorPlataforma().ToPagedList(pageNumber, pageSize),
                NumeroClientesPorPais = logica.lstNumeroClientesPorPais().ToPagedList(pageNumber, pageSize)
            };

            return View(model);
        }

    }
}