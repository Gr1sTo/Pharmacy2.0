using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("goods_in_storage")]
    public class GoodsInStorage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Medicine")]
        [Column("article")]
        public int Article { get; set; }

        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [Column("availability")]
        public bool Availability { get; set; }

        [ForeignKey("Storage")]
        [Column("storage_id")]
        public int StorageId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Storage Storage { get; set; }
    }

}
