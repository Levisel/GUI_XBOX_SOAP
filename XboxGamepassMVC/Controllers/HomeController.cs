using LazyCache;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using XboxGamepassMVC.Logica;
using XboxGamepassMVC.Models;

namespace XboxGamepassMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly LogicaSuscripciones _contextSuscripcion = new LogicaSuscripciones();
        private readonly LogicaPagos _contextPago = new LogicaPagos();
        private IAppCache cache = new CachingService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult Planes()
        {
            return View();
        }

        public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult PurchaseComplete()
        {
            return View();
        }

        public DateTime CalcularFechaFin(DateTime fechaFin, string duracion)
        {
            if (duracion.Equals("1 Mes"))
            {
                fechaFin = DateTime.Now.AddMonths(1);
            }
            else if (duracion.Equals("3 Meses"))
            {
                fechaFin = DateTime.Now.AddMonths(3);
            }
            else if (duracion.Equals("12 Meses"))
            {
                fechaFin = DateTime.Now.AddMonths(12);
            }
            return fechaFin;
        }

        [HttpPost]
        public ActionResult Comprar(string clienteId, string productosData, string subtotalData)
        {
            // Deserializar la cadena JSON para extraer el contenido del ID y la duración del plan
            var productosJson = JArray.Parse(productosData);
            var planId = productosJson[0]["id"].ToString(); // Extraer el ID del primer plan en la lista
            var planDuracion = productosJson[0]["duration"].ToString(); // Extraer la duración del primer plan en la lista

            if (string.IsNullOrEmpty(clienteId) || string.IsNullOrEmpty(planId) || string.IsNullOrEmpty(subtotalData) || string.IsNullOrEmpty(planDuracion))
            {
                // Manejar el error, redirigir a una página de error o mostrar un mensaje al usuario
                return RedirectToAction("Index", "Home");
            }

            var cont = 1; // Inicializamos el contador en 1
            var fecha = CalcularFechaFin(DateTime.Now, planDuracion);

            var suscripcionesCliente = cache.GetOrAdd("suscripcionesKey", () => _contextSuscripcion.ListarSuscripciones()
                .Where(s => s.Sus_ID.StartsWith("Sus-" + clienteId + "-"))
                .ToList(), DateTimeOffset.Now.AddHours(1));

            // Buscamos el último número de suscripción para ese cliente
            if (suscripcionesCliente.Any())
            {
                var ultimoContador = suscripcionesCliente
                    .Select(s => int.Parse(s.Sus_ID.Split('-').Last()))
                    .Max();

                cont = ultimoContador + 1;
            }

            var suscripcion = new ModelSuscripcion
            {
                Sus_ID = "Sus-" + clienteId + "-" + cont, // ID de suscripción único
                Plan_ID = int.Parse(planId), // ID del plan seleccionado
                Sus_StartDate = DateTime.Now,
                Sus_EndDate = fecha,
                Sus_Estado = "Inactivo",
                Sus_RenovacionAuto = false,
            };
            _contextSuscripcion.crearSuscripcion(suscripcion);
            cache.Remove("suscripcionesKey"); // Invalida la caché

            var pago = cache.GetOrAdd("pagoKey", () => new ModelPago
            {
                Cli_Cedula = clienteId,
                Sus_ID = suscripcion.Sus_ID,
                Pago_Monto = decimal.Parse(subtotalData),
                Pago_Fecha = suscripcion.Sus_StartDate,
                Pago_Estado = "Pendiente",
            }, DateTimeOffset.Now.AddHours(1));

            _contextPago.crearPago(pago);
            cache.Remove("pagoKey"); // Invalida la caché
            cache.Remove("pagosKey"); // Invalida la caché de la lista de pagos

            return RedirectToAction("PurchaseComplete", "Home");
        }
    }
}
