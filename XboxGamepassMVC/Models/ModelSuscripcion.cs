using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace XboxGamepassMVC.Models
{
    public class ModelSuscripcion
    {
        [Required]
        [Key]
        [Display(Name = "ID")]
        public string Sus_ID { get; set; }

        [Required]
        [Display(Name = "Plan")]
        public int Plan_ID { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Sus_StartDate { get; set; }

        [Display(Name = "Fecha de Finalización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Sus_EndDate { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string Sus_Estado { get; set; }

        [Required]
        [Display(Name = "Renovación Automatica")]
        public Nullable<bool> Sus_RenovacionAuto { get; set; }

        public Nullable<bool> Sus_LogicalDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<ModelPago> Pagos { get; set; }
        public virtual ModelPlan Plan { get; set; }

        // Añade este atributo
        [NotMapped] // Esto asegura que el atributo no se mapeará a la base de datos
        [Display(Name = "Plan")]
        public string Plan_Nombre { get; set; }


    }
}