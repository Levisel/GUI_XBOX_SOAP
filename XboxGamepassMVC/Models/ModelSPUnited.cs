using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxGamepassMVC.Models
{
    public class ModelSPUnited
    {
        public IPagedList<ModelClientesConSuscripcionActivaInactiva> ClientesSuscripcionActivaInactiva { get; set; }
        public IPagedList<ModelClientesPorPaisPlan> ClientesPorPaisYPlan { get; set; }
        public IPagedList<ModelClientesPorPlataforma> ClientesPorPlataforma { get; set; }
        public IPagedList<ModelNumeroClientesPorPais> NumeroClientesPorPais { get; set; }
    }
}