using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirst.DB.Models
{
    [Table("OrderList")]
    class OrderList
    {
        [Key]
        public long orderListId { get; set; }
        [Required]
        [ForeignKey("order")]
        public long orderId { get; set; }
        public Order order { get; set; }
        [Required]
        [ForeignKey("product")]
        public long productId { get; set; }
        public Product product { get; set; }
        [Required]
        public int productQuantity { get; set; }
    }
}
