using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Data.Modeli {
    [Table("ZauzeteSobe")]
    public class ZauzetaSoba {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Soba))]
        public int SobaId{ get; set; }
        [JsonIgnore]
        public Soba Soba { get; set; }
        [Required]
        public int BrojOsoba { get; set; }
        [Required]
        public int BrojDjece { get; set; }
        [Required] 
        public float Cijena{ get; set; }
        [Required] 
        public DateTime DatumDolaska{ get; set; }
        [Required] 
        public DateTime DatumOdlaska { get; set; }
    }
}
