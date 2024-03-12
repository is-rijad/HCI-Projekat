using Backend.Data.Modeli;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints.Korisnici.Uloguj {
    [Route("Korisnici/UlogujSe")]
    public class UlogujKorisnikaEndpoint : BaseEndpoint<UlogujKorisnikaEndpointReq, UlogujKorisnikaEndpointRes> {
        private readonly AuthServis _authServis;

        public UlogujKorisnikaEndpoint(AuthServis authServis)
        {
            _authServis = authServis;
        }
        [HttpPost]
        public override async Task<UlogujKorisnikaEndpointRes> Akcija(UlogujKorisnikaEndpointReq req)
        {
            Tokeni token;
            var response = new UlogujKorisnikaEndpointRes();
            try
            {
                token = await _authServis.UlogujKorisnika(req);
                response.Token = token;
            }
            catch (Exception e)
            {
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
