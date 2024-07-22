using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XboxGamepassMVC.Logica
{
    public class LogicaSuscripciones
    {
        API_Suscripciones.API_SuscripcionesXbox _context = new API_Suscripciones.API_SuscripcionesXbox();
        LogicaPlanes logicaPlanes = new LogicaPlanes();
        public List<Models.ModelSuscripcion> ListarSuscripciones()
        {
            List<Models.ModelSuscripcion> listaSuscripciones = new List<Models.ModelSuscripcion>();

            var suscripcionesWS = _context.Listar();

            foreach (var suscripcionWS in suscripcionesWS)
            {
                Models.ModelSuscripcion suscripcion = new Models.ModelSuscripcion();
                suscripcion.Sus_ID = suscripcionWS.Sus_ID;
                suscripcion.Plan_ID = suscripcionWS.Plan_ID;
                suscripcion.Sus_StartDate = suscripcionWS.Sus_StartDate;
                suscripcion.Sus_EndDate = suscripcionWS.Sus_EndDate;
                suscripcion.Sus_Estado = suscripcionWS.Sus_Estado;
                suscripcion.Sus_RenovacionAuto = suscripcionWS.Sus_RenovacionAuto;
                suscripcion.Plan_Nombre = obtenerNombrePlan(suscripcion.Plan_ID);
                listaSuscripciones.Add(suscripcion);
            }

            return listaSuscripciones;
        }

        public Models.ModelSuscripcion buscarSuscripcion(string id)
        {
            var suscripcionWS = _context.buscarPorId(id);

            Models.ModelSuscripcion suscripcion = new Models.ModelSuscripcion();
            suscripcion.Sus_ID = suscripcionWS.Sus_ID;
            suscripcion.Plan_ID = suscripcionWS.Plan_ID;
            suscripcion.Sus_StartDate = suscripcionWS.Sus_StartDate;
            suscripcion.Sus_EndDate = suscripcionWS.Sus_EndDate;
            suscripcion.Sus_Estado = suscripcionWS.Sus_Estado;
            suscripcion.Sus_RenovacionAuto = suscripcionWS.Sus_RenovacionAuto;
            suscripcion.Plan_Nombre = obtenerNombrePlan(suscripcion.Plan_ID);

            return suscripcion;
        }

        public void crearSuscripcion(Models.ModelSuscripcion suscripcion)
        {
            API_Suscripciones.Suscripcion suscripcionWS = new API_Suscripciones.Suscripcion();
            suscripcionWS.Sus_ID = suscripcion.Sus_ID;
            suscripcionWS.Plan_ID = suscripcion.Plan_ID;
            suscripcionWS.Sus_StartDate = suscripcion.Sus_StartDate;
            suscripcionWS.Sus_EndDate = suscripcion.Sus_EndDate;
            suscripcionWS.Sus_Estado = suscripcion.Sus_Estado;
            suscripcionWS.Sus_RenovacionAuto = suscripcion.Sus_RenovacionAuto;
            suscripcion.Plan_Nombre = obtenerNombrePlan(suscripcion.Plan_ID);
            _context.Insertar(suscripcionWS);
        }

        public void actualizarSuscripcion(Models.ModelSuscripcion suscripcion)
        {
            API_Suscripciones.Suscripcion suscripcionWS = new API_Suscripciones.Suscripcion();
            suscripcionWS.Sus_ID = suscripcion.Sus_ID;
            suscripcionWS.Plan_ID = suscripcion.Plan_ID;
            suscripcionWS.Sus_StartDate = suscripcion.Sus_StartDate;
            suscripcionWS.Sus_EndDate = suscripcion.Sus_EndDate;
            suscripcionWS.Sus_Estado = suscripcion.Sus_Estado;
            suscripcionWS.Sus_RenovacionAuto = suscripcion.Sus_RenovacionAuto;
            suscripcion.Plan_Nombre = obtenerNombrePlan(suscripcion.Plan_ID);
            _context.Actualizar(suscripcionWS);
        }

        public void eliminarSuscripcion(string id)
        {
            _context.Eliminar(id);
        }

        public string obtenerNombrePlan(int id)
        {
            string nombre = logicaPlanes.obtenerNombrePlan(id);
            return nombre;
        }

        public List<KeyValuePair<int, string>> ObtenerPlanesConId()
        {
            return logicaPlanes.ObtenerPlanesConId();
        }

        public List<string> ObtenerIDsDeSuscripcion()
        {
            return _context.Listar().Select(s=> s.Sus_ID).Distinct().ToList();
        }
    }
}