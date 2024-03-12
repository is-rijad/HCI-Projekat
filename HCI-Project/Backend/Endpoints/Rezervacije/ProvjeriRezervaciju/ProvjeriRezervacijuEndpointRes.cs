using Backend.Data.Modeli;

namespace Backend.Endpoints.Rezervacije.ProvjeriRezervaciju;

public class ProvjeriRezervacijuEndpointRes : BaseResponse
{
    public ZauzetaSoba? DetaljiRezervacije { get; set; }
    public Soba? Soba { get; set; }
    public SobaAranzman? SobaAranzman { get; set; }
}