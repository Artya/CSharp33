using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EFCodeFirst.DB.Models
{
    [Table("Customers")]
    [DataContract]
    class Customer
    {
        [Key]
        [DataMember]
        public long customerId { get; set; }
        [Required]
        [StringLength(50)]
        [DataMember]
        public string cutomerName { get; set; }
        [Required]
        [StringLength(50)]
        [DataMember]
        public string customerEmail { get; set; }

    }
}
