using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    [Table("tblAfavorde")]
    public partial class tblAfavorde
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblAfavorde()
        {
            tblLibros = new HashSet<tblLibros>();
        }

        [Key]
        [Display(Name ="A/F")]
        public int idfavorde { get; set; }

        [Display(Name = "Nombres A/F")]
        [StringLength(60)]
        public string nombres { get; set; }

        [Display(Name = "Apellidos A/F")]
        [StringLength(60)]
        public string apellidos { get; set; }

        [Display(Name = "Red social")]
        [StringLength(20)]
        public string redsocial { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(9)]
        public string telefono { get; set; }

        [Display(Name = "Correo")]
        [StringLength(100)]
        public string correo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? fechanacimiento { get; set; }

        [Display(Name = "Lugar de nacimiento")]
        [StringLength(100)]
        public string lugarnacimiento { get; set; }

        [Display(Name = "Municipio")]
        [StringLength(60)]
        public string municipio { get; set; }

        [Display(Name = "Departamento")]
        [StringLength(30)]
        public string departamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLibros> tblLibros { get; set; }
    }
}