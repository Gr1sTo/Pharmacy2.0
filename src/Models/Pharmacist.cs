using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("pharmacist")]
    public class Pharmacist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Surname_Name_Patronymic")]
        public string SurnameNamePatronymic { get; set; }

        // Для зв'язку з Bill
        public virtual ICollection<Bill> Bills { get; set; }
    }

}
