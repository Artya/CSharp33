using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelloCodeFirstLinq
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FacultetID { get; set; }
        public Nullable<int> Size { get; set; }
        public Nullable<decimal> Marks { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public short Contract { get; set; 
        public virtual Faculty Faculty { get; set; }
    }
}
