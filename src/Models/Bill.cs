using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("bill")]
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [ForeignKey("Pharmacist")]
        [Column("ID_Pharmacist")]
        public int? IDPharmacist { get; set; }

        [Column("Discount_card_ID")]
        public int? DiscountCardID { get; set; }

        [Column("final_price")]
        public decimal FinalPrice { get; set; }

        [StringLength(10)]
        [Column("Type_of_pay")]
        public string TypeOfPay { get; set; }

        [Column("Date_of_buy")]
        public DateTime DateOfBuy { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Place")]
        public string Place { get; set; }

        public virtual Pharmacist Pharmacist { get; set; }

        // Для зв'язків
        public virtual ICollection<MedicineList> MedicineLists { get; set; }
        public virtual Recipe Recipe { get; set; }
    }

}
