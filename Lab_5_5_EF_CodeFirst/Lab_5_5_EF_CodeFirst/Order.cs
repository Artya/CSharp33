using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5_5_EF_CodeFirst
{
    [Table("Orders")]
    internal class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        
        [Required]
        [StringLength(512)]
        public string OrderDetails{ get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public ICollection<OrderList> OrderLists { get; set; }
    }
}
