using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace XboxGamepassMVC.Data
{
    public class XboxGamepassMVCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public XboxGamepassMVCContext() : base("name=XboxGamepassMVCContext")
        {
        }

        public System.Data.Entity.DbSet<XboxGamepassMVC.Models.ModelCliente> GUIClientes { get; set; }

        public System.Data.Entity.DbSet<XboxGamepassMVC.Models.ModelPlan> GUIPlanes { get; set; }

        public System.Data.Entity.DbSet<XboxGamepassMVC.Models.ModelSuscripcion> GUISuscripciones { get; set; }

        public System.Data.Entity.DbSet<XboxGamepassMVC.Models.ModelPago> GUIPagos { get; set; }
    }
}
