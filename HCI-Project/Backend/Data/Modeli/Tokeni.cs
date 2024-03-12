using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("Tokeni")]
    public class Tokeni {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Token { get; set; }
        [ForeignKey(nameof(KorisnickiNalog))] 
        public int KorisnickiNalogId{ get; set; }
        [NotMapped]
        public KorisnickiNalog? KorisnickiNalog{ get; set; }
        public DateTime Vrijeme { get; set; }
        public bool IsLogiran { get; set; }
    }
}
