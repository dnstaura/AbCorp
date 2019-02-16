using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblDepartamentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDepartamentos()
        {
            tblCasos = new HashSet<tblCasos>();
            tblJuzgados = new HashSet<tblJuzgados>();
            tblMunicipios = new HashSet<tblMunicipios>();
        }

        [Key]
        public int Id_Departamento { get; set; }

        [Required]
        [StringLength(50)]
        public string Departamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCasos> tblCasos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblJuzgados> tblJuzgados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMunicipios> tblMunicipios { get; set; }
    }
}