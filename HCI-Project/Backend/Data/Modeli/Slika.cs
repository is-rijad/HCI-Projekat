using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Data.Modeli {
    [Table("Slike")]
    public class Slika {
        public int Id { get; set; }
        
        [ForeignKey(nameof(Soba))]
        public int SobaId{ get; set; }

        [JsonIgnore]
        public Soba Soba { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
