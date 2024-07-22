using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace XboxGamepassMVC.Logica
{
    public class LogicaClientes
    {
        API_Clientes.API_XboxGamepass _context = new API_Clientes.API_XboxGamepass();

        public List<Models.ModelCliente> ListarClientes()
        {


            List<Models.ModelCliente> listaClientes = new List<Models.ModelCliente>();

            var clientesWS = _context.Listar();

            foreach(var clienteWS in clientesWS )
            {
                Models.ModelCliente cliente = new Models.ModelCliente();
                cliente.Cli_Cedula = clienteWS.Cli_Cedula;
                cliente.Cli_Nombre = clienteWS.Cli_Nombre;
                cliente.Cli_Apellido = clienteWS.Cli_Apellido;
                cliente.Cli_BirthDate = clienteWS.Cli_BirthDate;
                cliente.Cli_Pais = clienteWS.Cli_Pais;
                cliente.Cli_Email = clienteWS.Cli_Email;
                cliente.Cli_LogicalDelete = clienteWS.Cli_LogicalDelete;

                listaClientes.Add(cliente);
            }
            
            return listaClientes;
        }

        public Models.ModelCliente buscarCliente(string cedula)
        {
            var clienteWS = _context.buscarPorId(cedula);

            Models.ModelCliente cliente = new Models.ModelCliente();
            cliente.Cli_Cedula = clienteWS.Cli_Cedula;
            cliente.Cli_Nombre = clienteWS.Cli_Nombre;
            cliente.Cli_Apellido = clienteWS.Cli_Apellido;
            cliente.Cli_BirthDate = clienteWS.Cli_BirthDate;
            cliente.Cli_Pais = clienteWS.Cli_Pais;
            cliente.Cli_Email = clienteWS.Cli_Email;

            return cliente;
        }

        public void crearCliente(Models.ModelCliente cliente)
        {
            API_Clientes.Cliente clienteWS = new API_Clientes.Cliente();
            clienteWS.Cli_Cedula = cliente.Cli_Cedula;
            clienteWS.Cli_Nombre = cliente.Cli_Nombre;
            clienteWS.Cli_Apellido = cliente.Cli_Apellido;
            clienteWS.Cli_BirthDate = cliente.Cli_BirthDate;
            clienteWS.Cli_Pais = cliente.Cli_Pais;
            clienteWS.Cli_Email = cliente.Cli_Email;
            _context.Insertar(clienteWS);
        }

        public void actualizarCliente(Models.ModelCliente cliente)
        {
            API_Clientes.Cliente clienteWS = new API_Clientes.Cliente();
            clienteWS.Cli_Cedula = cliente.Cli_Cedula;
            clienteWS.Cli_Nombre = cliente.Cli_Nombre;
            clienteWS.Cli_Apellido = cliente.Cli_Apellido;
            clienteWS.Cli_BirthDate = cliente.Cli_BirthDate;
            clienteWS.Cli_Pais = cliente.Cli_Pais;
            clienteWS.Cli_Email = cliente.Cli_Email;
            _context.Actualizar(clienteWS);
        }

        public void eliminarCliente(string cedula)
        {
            _context.Eliminar(cedula);
        }

        public string obtenerNombreCliente(string id)
        {
            var cliente = _context.buscarPorId(id); 

            if (cliente != null)
            {
                return cliente.Cli_Nombre + " " + cliente.Cli_Apellido;
            }

            return string.Empty;
        }

        public List<KeyValuePair<string, string>> ObtenerClientesConId()
        {
            return _context.Listar().Select(c => new KeyValuePair<string, string>(c.Cli_Cedula, c.Cli_Nombre + " " + c.Cli_Apellido)).ToList();
        }


    }
}