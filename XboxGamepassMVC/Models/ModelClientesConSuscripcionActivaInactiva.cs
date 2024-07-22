using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxGamepassMVC.Models
{
    public class ModelClientesConSuscripcionActivaInactiva
    {
        public Nullable<int> ClientesConSuscripcionActiva { get; set; }
        public Nullable<int> ClientesSinSuscripcionActiva { get; set; }
    }
}