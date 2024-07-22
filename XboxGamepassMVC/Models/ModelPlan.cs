using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace XboxGamepassMVC.Models
{
    public class ModelPlan
    {
        [Required]
        [Key]
        [JsonProperty("id")]
        public int Plan_ID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [JsonProperty("title")]
        public string Plan_Nombre { get; set; }

        [Required]
        [Display(Name = "Duración")]
        [JsonProperty("duration")]
        public string Plan_Duracion { get; set; }

        [Required]
        [Display(Name = "Precio")]
        [JsonProperty("price")]
        public Nullable<decimal> Plan_Precio { get; set; }

        [Required]
        [Display(Name = "Plataforma")]
        public string Plan_Plataforma { get; set; }


        public Nullable<bool> Plan_LogicalDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<ModelSuscripcion> Suscripcions { get; set; }




    }
}