using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Data.Modeli {
    [Table("SobeAranzmani")]
    public class SobaAranzman {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Soba))]
        public int SobaId { get; set; }
        [JsonIgnore]
        public Soba Soba { get; set; }
        [ForeignKey(nameof(Aranzman))]
        public int AranzmanId { get; set; }

        public Aranzman Aranzman { get; set; }
        [Required]
        public float Doplata { get; set; }
    }
}
