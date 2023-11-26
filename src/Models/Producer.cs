using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.Models
{
    [Table("producer")]
    public class Producer
    {
        [Key]
        [Column("code")]
        public int Code { get; set; }

        [Required]
        [StringLength(20)]
        [Column("name")]
        public string Name { get; set; }

        [Column("licenseNum")]
        public int LicenseNum { get; set; }

        // Для зв'язку з Medicine
        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
