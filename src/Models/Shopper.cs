using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.src.Models
{

    [Table("shopper")]
    public class Shopper
    {
        [Key]
        [StringLength(12)]
        [Column("phoneNum")]
        public string PhoneNum { get; set; }

        [Column("discountValue")]
        public float DiscountValue { get; set; }

        [StringLength(15)]
        [Column("ID_discountcard")]
        public string IDDiscountCard { get; set; }

        // властивість для зв'язку з Purchase
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}