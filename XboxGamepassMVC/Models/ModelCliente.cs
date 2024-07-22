using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace XboxGamepassMVC.Models
{
    public class ModelCliente
    {

        [Required]
        [Key]
        [Display(Name = "Cédula")]
        public string Cli_Cedula { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Cli_Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Cli_Apellido { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Cli_BirthDate { get; set; }

        [Required]
        [Display(Name = "País")]
        public string Cli_Pais { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Ingresa un formato de correo electrónico válido.")]
        public string Cli_Email { get; set; }
        public Nullable<bool> Cli_LogicalDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<ModelPago> Pagos { get; set; }




    }
}