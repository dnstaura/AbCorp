using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblEstadosRegistros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblEstadosRegistros()
        {
            tblClientes = new HashSet<tblClientes>();
            tblPersonal = new HashSet<tblPersonal>();
        }

        [Key]
        [Display(Name = "Estado de registro")]
        public int Id_EstadoRegistro { get; set; }

        [Required]
        [Display(Name = "Estado de registro")]
        [StringLength(50)]
        public string Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblClientes> tblClientes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPersonal> tblPersonal { get; set; }
    }
}