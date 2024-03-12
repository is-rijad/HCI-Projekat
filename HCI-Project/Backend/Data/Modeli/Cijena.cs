using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Data.Modeli;

[Table("Cijene")]
public class Cijena
{
    [Key] public int? Id { get; set; }

    [ForeignKey(nameof(Soba))] public int SobaId { get; set; }

    [JsonIgnore] public Soba? Soba { get; set; }

    public int BrojOsoba { get; set; }
    public float CijenaSobe { get; set; }
}