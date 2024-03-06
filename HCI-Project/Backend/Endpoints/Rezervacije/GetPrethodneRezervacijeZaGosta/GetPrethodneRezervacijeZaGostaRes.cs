using Backend.Data.Modeli;

namespace Backend.Endpoints.Rezervacije.GetBuduceRezervacijeZaGosta {
    public class GetPrethodneRezervacijeZaGostaRes : BaseResponse {
        public List<RezervacijaModel>? Rezervacije { get; set; }

    }
}
