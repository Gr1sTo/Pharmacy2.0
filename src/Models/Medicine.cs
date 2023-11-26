using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("medicine")]
    public class Medicine
    {
        [Key]
        [Column("Article")]
        public int Article { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Active_substance")]
        public string ActiveSubstance { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Column("Country")]
        public string Country { get; set; }

        [Column("Expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(20)]
        [Column("method_of_administration")]
        public string MethodOfAdministration { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Release_form")]
        public string ReleaseForm { get; set; }

        [Column("cnt_in_pack")]
        public int CntInPack { get; set; }

        [Column("receipe_need")]
        public bool ReceipeNeed { get; set; }

        [Column("temperature")]
        public int Temperature { get; set; }

        [StringLength(50)]
        [Column("packing")]
        public string Packing { get; set; }

        [ForeignKey("Producer")]
        [Column("producerCode")]
        public int? ProducerCode { get; set; }

        public virtual Producer Producer { get; set; }

        // Для зв'язків
        public virtual ICollection<MedicineList> MedicineLists { get; set; }
        public virtual GoodsInStorage GoodsInStorage { get; set; }
    }

}
