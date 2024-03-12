using Backend.Data.Modeli;

namespace Backend.Endpoints.Sobe.ModifikujSobu;

public class ModifikujSobuEndpointReq
{
    public Soba Soba { get; set; }
    public List<SobaKrevet> Kreveti { get; set; }
    public List<SobaAranzman> Aranzmani { get; set; }
    public List<Cijena> Cijene { get; set; }
}