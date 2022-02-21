using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirst.DB.Models
{
    [Table("Orders")]
    class Order
    {
        [Key]
        public long orderId { get; set; }
        [Required]
        [ForeignKey("customer")]
        public long customerId { get; set; }
        public Customer customer { get; set; }
        [Required]
        public bool orderStatus { get; set; }
        [Required]
        public string orderDetails { get; set; }
        [Required]
        public DateTime orderDate { get; set; }
    }
}
