using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    [Table("tblHistorial")]
    public partial class tblHistorial
    {
        [Key]
        public int Id_Historial { get; set; }

        public int Id_Caso { get; set; }

        [Column(TypeName = "date")]
        [Display(Name ="Fecha Inicio")]
        public DateTime Fecha_Inicio { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha Final")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Final { get; set; }

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(100)]
        public string Archivo { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha Agregado")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Agregado { get; set; }

        public virtual tblCasos tblCasos { get; set; }
    }
}