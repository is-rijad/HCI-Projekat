using Backend.Data;
using Backend.Endpoints.Rezervacije.ProvjeriRezervaciju;
using Backend.Filteri;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints.Rezervacije.NapraviRezervaciju;

[Route("Rezervacije/NapraviRezervaciju")]
public class NapraviRezervacijuEndpoint : BaseEndpoint<NapraviRezervacijuEndpointReq, BaseResponse>
{
    private readonly AuthServis _authServis;
    private readonly HCIDBContext _dbContext;
    private readonly ProvjeriRezervaciju _provjeriRezervaciju;

    public NapraviRezervacijuEndpoint(HCIDBContext context,
        ProvjeriRezervaciju provjeriRezervaciju,
        AuthServis authServis)
    {
        _dbContext = context;
        _provjeriRezervaciju = provjeriRezervaciju;
        _authServis = authServis;
    }

    [AuthFilter]
    [HttpPost]
    public override async Task<BaseResponse> Akcija(NapraviRezervacijuEndpointReq req)
    {
        var token = _authServis.GetCookie();
        var gostId = token!.KorisnickiNalogId;
        var sobaDostupna = await _provjeriRezervaciju.Provjeri(req);
        var response = sobaDostupna as ProvjeriRezervacijuEndpointRes;
        if (response == null)
        {
            return sobaDostupna;
        }

        response.DetaljiRezervacije.GostId = gostId;
        await _dbContext.ZauzeteSobe.AddAsync(response.DetaljiRezervacije!);
        await _dbContext.SaveChangesAsync();
        return new BaseResponse { Message = "Rezervacija je uspješno napravljena!" };
    }
}