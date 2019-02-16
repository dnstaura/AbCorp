using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    [Table("tblEventCalendar")]
    public partial class tblEventCalendar
    {
        [Key]
        public int id_Event { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string descripcion { get; set; }

        [Required]
        [StringLength(255)]
        public string color { get; set; }

        [Required]
        [StringLength(255)]
        public string textColor { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }
    }
}