using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxGamepassMVC.Logica
{
    public class LogicaSP
    {
        API_StoredProcedures.API_StoredProceduresXbox _context = new API_StoredProcedures.API_StoredProceduresXbox();

        public List<Models.ModelClientesConSuscripcionActivaInactiva> lstSuscripcionActivaInactiva()
        {
            List<Models.ModelClientesConSuscripcionActivaInactiva> lstSuscripcionActiva = new List<Models.ModelClientesConSuscripcionActivaInactiva>();

            var storedProcedure = _context.ListarClientesConSuscripcionActivaInactiva();

            foreach(var procedureWS in storedProcedure)
            {
                Models.ModelClientesConSuscripcionActivaInactiva _procedure = new Models.ModelClientesConSuscripcionActivaInactiva();
                _procedure.ClientesConSuscripcionActiva = procedureWS.ClientesConSuscripcionActiva;
                _procedure.ClientesSinSuscripcionActiva = procedureWS.ClientesSinSuscripcionActiva;
                lstSuscripcionActiva.Add(_procedure);
            }
            return lstSuscripcionActiva;
        }

        public List<Models.ModelClientesPorPaisPlan> lstClientesPaisPlan()
        {
            List<Models.ModelClientesPorPaisPlan> lstClientesPorPaisPlan = new List<Models.ModelClientesPorPaisPlan>();

            var storedProcedure = _context.ListarClientesPorPaisYPlan();

            foreach(var procedureWS in storedProcedure)
            {
                Models.ModelClientesPorPaisPlan _procedure = new Models.ModelClientesPorPaisPlan();
                _procedure.Pais = procedureWS.Pais;
                _procedure.NumeroClientes = procedureWS.NumeroClientes;
                _procedure.NombrePlan = procedureWS.NombrePlan;
                _procedure.DuracionPlan = procedureWS.DuracionPlan;
                lstClientesPorPaisPlan.Add(_procedure);
            }
            return lstClientesPorPaisPlan;
        }

        public List<Models.ModelClientesPorPlataforma> lstClientesPorPlataforma()
        {

            List<Models.ModelClientesPorPlataforma> lstClientesPorPlataforma = new List<Models.ModelClientesPorPlataforma>();

            var storedProcedure = _context.ListarClientesPorPlataforma();

            foreach(var procedureWS in storedProcedure)
            {
                Models.ModelClientesPorPlataforma _procedure = new Models.ModelClientesPorPlataforma();
                _procedure.Plataforma = procedureWS.Plataforma;
                _procedure.NumeroClientes = procedureWS.NumeroClientes;
                lstClientesPorPlataforma.Add(_procedure);
            }
            return lstClientesPorPlataforma;

        }

        public List<Models.ModelNumeroClientesPorPais> lstNumeroClientesPorPais()
        {

            List<Models.ModelNumeroClientesPorPais> lstClientesPorPais = new List<Models.ModelNumeroClientesPorPais>();

            var storedProcedure = _context.ListarNumeroClientesPorPais();

            foreach(var procedureWS in storedProcedure)
            {
                Models.ModelNumeroClientesPorPais _procedure = new Models.ModelNumeroClientesPorPais();
                _procedure.Pais = procedureWS.Pais;
                _procedure.NumeroClientes = procedureWS.NumeroClientes;
                lstClientesPorPais.Add(_procedure);
            }
            return lstClientesPorPais;
        }


    }
}