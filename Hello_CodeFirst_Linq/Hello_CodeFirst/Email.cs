using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelloCodeFirstLinq
{   
    [Table("Emails")]
    public class Email
    {
        [Key]
        public int ID { get; set; }
        public string EmailValue { get; set; }
        public string LecturerID { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public override string ToString()
        {
            return $"{this.EmailValue}";
        }
    }
}
