using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace HelloCodeFirstLinq
{
    [Table("Lecturers")]
   public class Lecturer
    {
        [Key]
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<CourseLecturer> CourseLecturers { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"{this.ID} {this.FirstName} {this.LastName} {this.Phone} {this.Address} {this.State} {this.Zip}");

            foreach (var email in this.Emails)
            {
                builder.Append($", {email}");
            }

            return builder.ToString();
        }
    }
}
