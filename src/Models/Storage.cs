using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("storage")]
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        [Column("type_")]
        public string Type { get; set; }

        [Column("shelfCapacity")]
        public int ShelfCapacity { get; set; }

        // Для зв'язку з GoodsInStorage
        public virtual ICollection<GoodsInStorage> Goods { get; set; }
    }

}
