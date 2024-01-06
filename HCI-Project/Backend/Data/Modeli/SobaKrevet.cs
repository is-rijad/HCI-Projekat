using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("SobeKreveti")]
    public class SobaKrevet {
        [Key] 
        public int Id { get; set; }

        [ForeignKey(nameof(Soba))]
        public int SobaId { get; set; }
        public Soba Soba { get; set; }
        [ForeignKey(nameof(Krevet))]
        public int KrevetId { get; set; }
        public Krevet Krevet { get; set; }

    }
}
