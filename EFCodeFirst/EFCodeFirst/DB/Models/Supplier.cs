using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirst.DB.Models
{
    [Table("Suppliers")]
    class Supplier
    {
        [Key]
        public long supplierId { get; set; }
        [Required]
        [StringLength(50)]
        public string supplierName { get; set; }
        [Required]
        [StringLength(18)]
        public string supplierPhone { get; set; }
        [Required]
        [StringLength(50)]
        public string supplierEmail { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
