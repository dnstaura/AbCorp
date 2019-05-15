using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblCasos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCasos()
        {
            tblArchivos = new HashSet<tblArchivos>();
            tblHistorial = new HashSet<tblHistorial>();
        }

        [Key]
        public int Id_Caso { get; set; }

        public int Id_Cliente { get; set; }

        public int Id_Categoria { get; set; }

        public int Id_EstadoCaso { get; set; }

        public int? Id_Departamento { get; set; }

        public int? Id_Municipio { get; set; }

        public int? Id_Juzgado { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="Número de Caso")]
        public string Numero_Caso { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Inicio { get; set; }

        [Display(Name = "Fecha de Audiencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Audiencia { get; set; }

        [Display(Name = "Fecha de Agregado")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Agregado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblArchivos> tblArchivos { get; set; }

        public virtual tblCategorias tblCategorias { get; set; }

        public virtual tblClientes tblClientes { get; set; }

        public virtual tblDepartamentos tblDepartamentos { get; set; }

        public virtual tblEstadosCasos tblEstadosCasos { get; set; }

        public virtual tblJuzgados tblJuzgados { get; set; }

        public virtual tblMunicipios tblMunicipios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHistorial> tblHistorial { get; set; }
    }
}