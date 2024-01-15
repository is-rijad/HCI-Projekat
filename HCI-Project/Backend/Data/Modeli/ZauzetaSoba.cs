using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("ZauzeteSobe")]
    public class ZauzetaSoba {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Soba))]
        public int SobaId{ get; set; }
        public Soba Soba { get; set; }
        [Required]
        public string BrojOsoba { get; set; }
        [Required] 
        public float Cijena{ get; set; }
        [Required] 
        public DateTime DatumDolaska{ get; set; }
        [Required] 
        public DateTime DatumOdlaska { get; set; }
    }
}
