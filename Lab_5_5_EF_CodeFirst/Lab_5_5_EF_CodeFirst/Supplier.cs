using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5_5_EF_CodeFirst
{
    [Table("Suppliers")]
    internal class Supplier
    {
        [Key]
        public int ID { get; set; }
       
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Phone { get; set; }
       
        [Required]
        public string Email { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
