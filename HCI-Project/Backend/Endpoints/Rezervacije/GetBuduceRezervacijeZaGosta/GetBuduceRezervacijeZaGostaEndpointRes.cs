using Backend.Data.Modeli;

namespace Backend.Endpoints.Rezervacije.GetBuduceRezervacijeZaGosta {
    public class GetBuduceRezervacijeZaGostaEndpointRes : BaseResponse {
        public List<RezervacijaModel>? Rezervacije { get; set; }
    }

    public class RezervacijaModel
    {
        public ZauzetaSoba? Rezervacija { get; set; }
        public string? NazivSobe { get; set; }
        public string? OpisSobe { get; set; }
        public string? Aranzman { get; set; }
    }
}
