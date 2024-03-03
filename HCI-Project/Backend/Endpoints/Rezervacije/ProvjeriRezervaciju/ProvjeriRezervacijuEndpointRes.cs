using Backend.Data.Modeli;

namespace Backend.Endpoints.Rezervacije.NapraviRezervaciju
{
    public class ProvjeriRezervacijuEndpointRes: BaseResponse
    {
        public ZauzetaSoba? DetaljiRezervacije { get; set; }
        public Soba? Soba { get; set; }
        public SobaAranzman? SobaAranzman { get; set; }
    }
}
