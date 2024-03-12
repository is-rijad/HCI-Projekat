using Backend.Data.Modeli;

namespace Backend.Endpoints.Korisnici.Uloguj {
    public class UlogujKorisnikaEndpointRes  : BaseResponse{
        public Tokeni Token { get; set; }
    }
}
