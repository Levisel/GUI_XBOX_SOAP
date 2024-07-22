using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxGamepassMVC.Logica
{
    public class LogicaPagos
    {
        API_Pagos.API_PagosXbox _context = new API_Pagos.API_PagosXbox();
        LogicaClientes logicaClientes = new LogicaClientes();
        LogicaSuscripciones logicaSuscripciones = new LogicaSuscripciones();

        public List<Models.ModelPago> ListarPagos()
        {
            List<Models.ModelPago> listaPagos = new List<Models.ModelPago>();

            var pagosWS = _context.Listar();

            foreach (var pagoWS in pagosWS)
            {
                Models.ModelPago pago = new Models.ModelPago();
                pago.Pago_ID = pagoWS.Pago_ID;
                pago.Pago_Codigo = pagoWS.Pago_Codigo;
                pago.Cli_Cedula = pagoWS.Cli_Cedula;
                pago.Sus_ID = pagoWS.Sus_ID;
                pago.Pago_Monto = pagoWS.Pago_Monto;
                pago.Pago_Fecha = pagoWS.Pago_Fecha;
                pago.Pago_Estado = pagoWS.Pago_Estado;
                pago.Cliente_Nombre = obtenerNombreCliente(pago.Cli_Cedula);
                listaPagos.Add(pago);
            }

            return listaPagos;
        }

        public Models.ModelPago buscarPago(int id)
        {
            var pagoWS = _context.buscarPorId(id);

            Models.ModelPago pago = new Models.ModelPago();
            pago.Pago_ID = pagoWS.Pago_ID;
            pago.Pago_Codigo = pagoWS.Pago_Codigo;
            pago.Cli_Cedula = pagoWS.Cli_Cedula;
            pago.Sus_ID = pagoWS.Sus_ID;
            pago.Pago_Monto = pagoWS.Pago_Monto;
            pago.Pago_Fecha = pagoWS.Pago_Fecha;
            pago.Pago_Estado = pagoWS.Pago_Estado;
            pago.Cliente_Nombre = obtenerNombreCliente(pago.Cli_Cedula);

            return pago;
        }

        public void crearPago(Models.ModelPago pago)
        {
            API_Pagos.Pago pagoWS = new API_Pagos.Pago();
            pagoWS.Pago_ID = pago.Pago_ID;
            pagoWS.Pago_Codigo = pago.Pago_Codigo;
            pagoWS.Cli_Cedula = pago.Cli_Cedula;
            pagoWS.Sus_ID = pago.Sus_ID;
            pagoWS.Pago_Monto = pago.Pago_Monto;
            pagoWS.Pago_Fecha = pago.Pago_Fecha;
            pagoWS.Pago_Estado = pago.Pago_Estado;
            pago.Cliente_Nombre = obtenerNombreCliente(pago.Cli_Cedula);
            _context.Insertar(pagoWS);
        }

        public void actualizarPago(Models.ModelPago pago)
        {
            API_Pagos.Pago pagoWS = new API_Pagos.Pago();
            pagoWS.Pago_ID = pago.Pago_ID;
            pagoWS.Cli_Cedula = pago.Cli_Cedula;
            pagoWS.Sus_ID = pago.Sus_ID;
            pagoWS.Pago_Monto = pago.Pago_Monto;
            pagoWS.Pago_Fecha = pago.Pago_Fecha;
            pagoWS.Pago_Estado = pago.Pago_Estado;
            pago.Cliente_Nombre = obtenerNombreCliente(pago.Cli_Cedula);
            _context.Actualizar(pagoWS);
        }



        public void eliminarPago(int id)
        {
            _context.Eliminar(id);
        }

        public string obtenerNombreCliente(string id)
        {
            return logicaClientes.obtenerNombreCliente(id);
        }


        public List<KeyValuePair<string, string>> ObtenerClientesConId()
        {
            return logicaClientes.ObtenerClientesConId();
        }

        public List<string> ObtenerIDsDeSuscripcion()
        {
            return logicaSuscripciones.ObtenerIDsDeSuscripcion();   
        }
    }
}