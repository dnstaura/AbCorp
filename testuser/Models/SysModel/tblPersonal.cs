using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    [Table("tblPersonal")]
    public partial class tblPersonal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPersonal()
        {
            tblLibros = new HashSet<tblLibros>();
        }

        [Key]
        [Display(Name = "Personal")]
        public int Id_Personal { get; set; }

        [Required]
        [Display(Name = "Nombre P/L")]
        [StringLength(50)]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Apellidos P/L")]
        [StringLength(50)]
        public string Apellidos { get; set; }

        [Display(Name = "Red social")]
        [StringLength(50)]
        public string redsocial { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [StringLength(50)]
        public string Telefono { get; set; }

        [Display(Name = "Correo")]
        [StringLength(50)]
        public string Correo { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(100)]
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de nacimiento")]
        [Column(TypeName = "date")]
        public DateTime Fecha_Nacimiento { get; set; }

        public int id_CategoriaPersonal { get; set; }

        public int? Id_EstadoRegistro { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de agregado")]
        [Column(TypeName = "date")]
        public DateTime Fecha_Agregado { get; set; }

        public virtual tblCategoriaPersonal tblCategoriaPersonal { get; set; }

        public virtual tblEstadosRegistros tblEstadosRegistros { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLibros> tblLibros { get; set; }
    }
}