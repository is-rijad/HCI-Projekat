using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("Kreveti")]
    public class Krevet {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tip { get; set; }
        [Required]
        public int ZaOsoba { get; set; }
    }
}
