using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblMunicipios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMunicipios()
        {
            tblCasos = new HashSet<tblCasos>();
            tblJuzgados = new HashSet<tblJuzgados>();
            tblAfavorde = new HashSet<tblAfavorde>();
            tblOtorgante = new HashSet<tblOtorgante>();
        }

        [Key]
        public int id_Municipio { get; set; }

        [Required]
        [StringLength(50)]
        public string Municipio { get; set; }

        public int Id_Departamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCasos> tblCasos { get; set; }

        public virtual tblDepartamentos tblDepartamentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblJuzgados> tblJuzgados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAfavorde> tblAfavorde { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOtorgante> tblOtorgante { get; set; }
    }
}