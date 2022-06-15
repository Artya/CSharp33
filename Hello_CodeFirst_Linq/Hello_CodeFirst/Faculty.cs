using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelloCodeFirstLinq
{
    [Table("Faculties")]
    public class Faculty
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string University { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
