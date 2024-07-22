using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxGamepassMVC.Models
{
    public class ModelClientesPorPlataforma
    {
        public string Plataforma { get; set; }
        public Nullable<int> NumeroClientes { get; set; }
    }
}