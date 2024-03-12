using Backend.Endpoints.Rezervacije.GetBuduceRezervacijeZaGosta;

namespace Backend.Endpoints.Rezervacije.GetPrethodneRezervacijeZaGosta;

public class GetPrethodneRezervacijeZaGostaRes : BaseResponse
{
    public List<RezervacijaModel>? Rezervacije { get; set; }
}