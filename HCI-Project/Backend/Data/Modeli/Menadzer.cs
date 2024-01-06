using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("Menadzeri")]
    public class Menadzer : KorisnickiNalog {
        [Key]
        public int Id { get; set; }
    }
}
