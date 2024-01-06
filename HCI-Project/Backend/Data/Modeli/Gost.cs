using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("Gosti")]
    public class Gost : KorisnickiNalog {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BrojRezervacija { get; set; }
        [Required]
        public string Drzava { get; set; }
        [Required]
        public string Grad { get; set; }
    }
}
