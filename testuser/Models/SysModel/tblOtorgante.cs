using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    [Table("tblOtorgante")]
    public partial class tblOtorgante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOtorgante()
        {
            tblLibros = new HashSet<tblLibros>();
        }

        [Key]
        [Display(Name = "Otorgante")]
        public int idotorgante { get; set; }

        [Display(Name = "Nombres otorgante")]
        [StringLength(60)]
        public string nombres { get; set; }

        [Display(Name = "Apellidos otorgante")]
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime fechanacimiento { get; set; }

        [Display(Name = "Lugar de Nacimiento")]
        [StringLength(100)]
        public string lugarnacimiento { get; set; }

        public int Id_Municipio { get; set; }

        public int Id_Departamento { get; set; }

        public virtual tblDepartamentos tblDepartamentos { get; set; }

        public virtual tblMunicipios tblMunicipios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLibros> tblLibros { get; set; }
    }
}