using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("recipe")]
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [ForeignKey("Bill")]
        [Column("ID_bill")]
        public int IDBill { get; set; }

        public virtual Bill Bill { get; set; }
    }

}
