using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5_5_EF_CodeFirst
{
    [Table("OrderList")]
    internal class OrderList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public Order Order { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public double Quantity { get; set; }
    }
}
