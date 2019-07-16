using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblEditables
    {
        [Key]
        public int idEditable { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Display(Name = "Entidad/Persona")]
        [StringLength(100)]
        public string entidad { get; set; }

        [Display(Name = "Instrumento")]
        [StringLength(100)]
        public string instrumento { get; set; }

        [Display(Name = "Documento")]
        [StringLength(100)]
        public string documento { get; set; }

        [Display(Name = "Tipo de Documento")]
        [StringLength(100)]
        public string tipo_documento { get; set; }

        [Display(Name = "Archivo Editable")]
        public string img { get; set; }

        [Display(Name ="Descripción")]
        [StringLength(1000)]
        public string descripcion { get; set; }
        


    }
}