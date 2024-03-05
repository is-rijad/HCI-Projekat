using Backend.Data;
using Backend.Data.Modeli;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Rezervacije.NapraviRezervaciju
{
    [Route("Rezervacije/NapraviRezervaciju")]
    public class NapraviRezervacijuEndpoint : BaseEndpoint<NapraviRezervacijuEndpointReq, BaseResponse>
    {
        private readonly HCIDBContext _dbContext;
        private readonly ProvjeriRezervaciju _provjeriRezervaciju;

        public NapraviRezervacijuEndpoint(HCIDBContext context,
            ProvjeriRezervaciju provjeriRezervaciju)
        {
            _dbContext = context;
            _provjeriRezervaciju = provjeriRezervaciju;
        }
        [HttpPost]
        public override async Task<BaseResponse> Akcija(NapraviRezervacijuEndpointReq req)
        {
            var sobaDostupna = await _provjeriRezervaciju.Provjeri(req);
            var response = sobaDostupna as ProvjeriRezervacijuEndpointRes;
            if (response == null) {
                return sobaDostupna;
            }
            else
            {
                await _dbContext.ZauzeteSobe.AddAsync(response.DetaljiRezervacije!);
                await _dbContext.SaveChangesAsync();
                return new BaseResponse() { Message = "Rezervacija je uspješno napravljena!" };
            }
        }
    }
}
