using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("medicineList")]
    public class MedicineList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [ForeignKey("Medicine")]
        [Column("MedArticle")]
        public int MedArticle { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("count_")]
        public int Count { get; set; }

        [ForeignKey("Bill")]
        [Column("BillID")]
        public int? BillID { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Bill Bill { get; set; }
    }

}
