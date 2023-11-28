using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("purchase")]
    public class Purchase
    {
        [Key]
        [Column("Id_Purchase")]
        public int Id_Purchase { get; set; } 

        [ForeignKey("Shopper")]
        [Column("PhoneNum")]
        public string PhoneNum { get; set; }

        [ForeignKey("Bill")]
        [Column("ID_Bill")]
        public int IDBill { get; set; }

        public virtual Shopper Shopper { get; set; }
        public virtual Bill Bill { get; set; }
    }

}
