using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XboxGamepassMVC.API_Planes;

namespace XboxGamepassMVC.Logica
{
    public class LogicaPlanes
    {
        API_Planes.API_PlanesXbox _context = new API_Planes.API_PlanesXbox();

        public List<Models.ModelPlan> ListarPlanes()
        {
            List<Models.ModelPlan> listaPlanes = new List<Models.ModelPlan>();

            var planesWS = _context.Listar();

            foreach (var planWS in planesWS)
            {
                Models.ModelPlan plan = new Models.ModelPlan();
                plan.Plan_ID = planWS.Plan_ID;
                plan.Plan_Nombre = planWS.Plan_Nombre;
                plan.Plan_Duracion = planWS.Plan_Duracion;
                plan.Plan_Precio = planWS.Plan_Precio;
                plan.Plan_Plataforma = planWS.Plan_Plataforma;
                listaPlanes.Add(plan);
            }
            return listaPlanes;
        }

        public Models.ModelPlan buscarPlan(int id)
        {
            var planWS = _context.buscarPorId(id);

            Models.ModelPlan plan = new Models.ModelPlan();
            plan.Plan_ID = planWS.Plan_ID;
            plan.Plan_Nombre = planWS.Plan_Nombre;
            plan.Plan_Duracion = planWS.Plan_Duracion;
            plan.Plan_Precio = planWS.Plan_Precio;
            plan.Plan_Plataforma = planWS.Plan_Plataforma;

            return plan;
        }

        public void crearPlan(Models.ModelPlan plan)
        {
            API_Planes.Plan planWS = new API_Planes.Plan();
            planWS.Plan_Nombre = plan.Plan_Nombre;
            planWS.Plan_Duracion = plan.Plan_Duracion;
            planWS.Plan_Precio = plan.Plan_Precio;
            planWS.Plan_Plataforma = plan.Plan_Plataforma;

            _context.Insertar(planWS);
        }

        public void actualizarPlan(Models.ModelPlan plan)
        {
            API_Planes.Plan planWS = new API_Planes.Plan();
            planWS.Plan_ID = plan.Plan_ID;
            planWS.Plan_Nombre = plan.Plan_Nombre;
            planWS.Plan_Duracion = plan.Plan_Duracion;
            planWS.Plan_Precio = plan.Plan_Precio;
            planWS.Plan_Plataforma = plan.Plan_Plataforma;

            _context.Actualizar(planWS);
        }

        public void eliminarPlan(int id)
        {
            _context.Eliminar(id);
        }

        public string obtenerNombrePlan(int id)
        {
            var plan = _context.buscarPorId(id);

            if (plan != null)
            {
                return plan.Plan_Nombre;
            }

            return string.Empty;
        }

        public List<KeyValuePair<int, string>> ObtenerPlanesConId()
        {
            return _context.Listar().Select(p => new KeyValuePair<int, string>(p.Plan_ID, p.Plan_Nombre)).ToList();
        }

    }
}