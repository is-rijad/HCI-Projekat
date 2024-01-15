using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli {
    [Table("Aranzmani")]
    public class Aranzman {
        [Key] 
        public int Id { get; set; }
        public string NazivAranzmana { get; set; }

    }
}
