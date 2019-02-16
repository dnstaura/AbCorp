using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    [Table("tblCategoriaPersonal")]
    public partial class tblCategoriaPersonal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCategoriaPersonal()
        {
            tblPersonal = new HashSet<tblPersonal>();
        }

        [Key]
        [Display(Name = "Categoria P/L")]
        public int id_CategoriaPersonal { get; set; }

        [Required]
        [Display(Name = "Categoria P/L")]
        [StringLength(100)]
        public string CategoriaPersonal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPersonal> tblPersonal { get; set; }
    }
}