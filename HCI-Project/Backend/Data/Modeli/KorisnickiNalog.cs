using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Backend.Data.Modeli {
    [Table("KorisnickiNalozi")]
    public class KorisnickiNalog {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Lozinka { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string SlikaKorisnika { get; set; }

        [JsonIgnore]
        public Gost? Gost => this as Gost;
        [JsonIgnore]
        public Menadzer? Menadzer => this as Menadzer;

        public bool IsGost => this.Gost != null;
        public bool IsMenadzer => this.Menadzer != null;
    }
}
