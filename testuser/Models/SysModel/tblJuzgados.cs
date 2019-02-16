using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblJuzgados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblJuzgados()
        {
            tblCasos = new HashSet<tblCasos>();
        }

        [Key]
        public int Id_Juzgado { get; set; }

        [Required]
        [StringLength(50)]
        public string Juzgado { get; set; }

        [Required]
        [StringLength(9)]
        public string telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string correo { get; set; }

        [Required]
        [StringLength(200)]
        public string direccion { get; set; }

        public int Id_Departamento { get; set; }

        public int Id_Municipio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCasos> tblCasos { get; set; }

        public virtual tblDepartamentos tblDepartamentos { get; set; }

        public virtual tblMunicipios tblMunicipios { get; set; }
    }
}