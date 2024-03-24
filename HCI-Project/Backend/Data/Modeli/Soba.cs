using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Modeli;

[Table("Sobe")]
public class Soba
{
    [Key] public int? Id { get; set; }

    [Required] public int BrojGostiju { get; set; }

    [Required] public string NazivSobe { get; set; }

    public string Opis { get; set; }
    public float DjecaDo { get; set; }
    public float CijenaZaDjecu { get; set; }


    public bool BesplatnoOtkazivanje { get; set; }
    public bool Klima { get; set; }
    public bool Bazen { get; set; }
    public bool Spa { get; set; }
    public bool PrilagodjenInvalidima { get; set; }
    public bool Teretana { get; set; }
    public bool DozvoljeniLjubimci { get; set; }
    public bool Minibar { get; set; }
    public bool Balkon { get; set; }

    public List<SobaKrevet>? Kreveti { get; set; }
    public List<SobaAranzman>? Aranzmani { get; set; }
    public List<ZauzetaSoba>? ZauzetaSoba { get; set; }
    public List<Cijena>? Cijene { get; set; }
}