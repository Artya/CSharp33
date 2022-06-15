using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelloCodeFirstLinq
{
    [Table("CourseLecturers")]
    public class CourseLecturer
    {
        [Key]
        [Column(Order = 1)]
        public string ID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string LecturerID { get; set; }
        public short LecturerOrder { get; set; }
        public decimal Share { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public virtual Course Course { get; set; }
    }
}
