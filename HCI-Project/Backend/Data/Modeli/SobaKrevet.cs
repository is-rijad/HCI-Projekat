using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Data.Modeli;

[Table("SobeKreveti")]
public class SobaKrevet
{
    [Key] public int? Id { get; set; }

    [ForeignKey(nameof(Soba))] public int SobaId { get; set; }

    [JsonIgnore] public Soba? Soba { get; set; }

    [ForeignKey(nameof(Krevet))] public int KrevetId { get; set; }

    public Krevet? Krevet { get; set; }

    [Required] public int BrojKreveta { get; set; }
}