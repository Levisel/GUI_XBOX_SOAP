using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxGamepassMVC.Models
{
    public class ModelClientesPorPaisPlan
    {

        public string Pais { get; set; }
        public Nullable<int> NumeroClientes { get; set; }
        public string NombrePlan { get; set; }
        public string DuracionPlan { get; set; }
    }
}