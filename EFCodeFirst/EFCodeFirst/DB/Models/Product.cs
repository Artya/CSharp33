using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirst.DB.Models
{
    [Table("Products")]
    class Product
    {
        [Key]
        public long productId { get; set; }
        [Required]
        [StringLength(50)]
        public string productName { get; set; }
        [Required]
        public decimal productPrice { get; set; }
        [Required]
        [StringLength(50)]
        public string productType { get; set; }
        [Required]
        [ForeignKey("supplier")]
        public long supplierId { get; set; }
        public Supplier supplier { get; set; }
    }
}
