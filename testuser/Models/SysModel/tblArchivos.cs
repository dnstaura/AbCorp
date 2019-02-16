using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblArchivos
    {
        [Key]
        public int Id_Archivo { get; set; }

        public int Id_Caso { get; set; }

        [Required]
        [StringLength(100)]
        public string Archivo { get; set; }

        [StringLength(500)]
        public string Descripción { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Agregado { get; set; }

        public virtual tblCasos tblCasos { get; set; }
    }
}