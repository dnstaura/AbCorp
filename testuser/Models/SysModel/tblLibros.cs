﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class tblLibros
    {
        [Key]
        public int idlibros { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime? fecha { get; set; }

        [Display(Name = "Instrumento")]
        [StringLength(100)]
        public string instrumento { get; set; }

        [Display(Name = "Folio")]
        [StringLength(100)]
        public string folios { get; set; }

        [Display(Name = "Libro")]
        [StringLength(100)]
        public string libro { get; set; }

        [Display(Name = "Archivo PDF")]
        public string img { get; set; }

        [Display(Name = "Otorgante")]
        public int idotorgante { get; set; }

        [Display(Name = "A favor de")]
        public int idfavorde { get; set; }

        [Display(Name = "Notario")]
        public int id_Personal { get; set; }

        public virtual tblAfavorde tblAfavorde { get; set; }

        public virtual tblOtorgante tblOtorgante { get; set; }

        public virtual tblPersonal tblPersonal { get; set; }
    }
}