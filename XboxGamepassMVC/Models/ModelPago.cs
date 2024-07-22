using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace XboxGamepassMVC.Models
{
    public class ModelPago
    {
        [Required]
        [Key]
        public int Pago_ID { get; set; }

        [Display(Name = "Código")]
        public string Pago_Codigo { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public string Cli_Cedula { get; set; }

        [Required]
        [Display(Name = "Suscripción")]
        public string Sus_ID { get; set; }

        [Required]
        [Display(Name = "Monto")]
        public Nullable<decimal> Pago_Monto { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Pago_Fecha { get; set; }

        [Required]
        [Display(Name = "Estado del Pago")]
        public string Pago_Estado { get; set; }
        public Nullable<bool> Pago_LogicalDelete { get; set; }

        public virtual ModelCliente Cliente { get; set; }
        public virtual ModelSuscripcion Suscripcion { get; set; }

        // Añade este atributo
        [NotMapped] // Esto asegura que el atributo no se mapeará a la base de datos
        [Display(Name = "Cliente")]
        public string Cliente_Nombre { get; set; }





    }
}