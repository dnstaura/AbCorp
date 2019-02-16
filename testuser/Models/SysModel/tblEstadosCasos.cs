using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblEstadosCasos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblEstadosCasos()
        {
            tblCasos = new HashSet<tblCasos>();
        }

        [Key]
        public int Id_EstadoCaso { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre_Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCasos> tblCasos { get; set; }
    }
}