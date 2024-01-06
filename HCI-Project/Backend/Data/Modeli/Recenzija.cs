using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("Recenzije")]
    public class Recenzija {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Soba))] 
        public int SobaId { get; set; }
        public Soba Soba { get; set; }
        [ForeignKey(nameof(Gost))]
        [Required]
        public int GostId { get; set; }
        public Gost Gost { get; set; }
        [Required]
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
        public DateTime Datum { get; set; }
    }
}
