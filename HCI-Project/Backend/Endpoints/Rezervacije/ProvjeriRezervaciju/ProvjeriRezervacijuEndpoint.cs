using Backend.Data;
using Backend.Endpoints.Rezervacije.NapraviRezervaciju;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints.Rezervacije.ProvjeriRezervaciju;

[Route("Rezervacije/ProvjeriRezervaciju")]
public class ProvjeriRezervacijuEndpoint : BaseEndpoint<NapraviRezervacijuEndpointReq, BaseResponse>
{
    private readonly HCIDBContext _dbContext;
    private readonly ProvjeriRezervaciju _provjeriRezervaciju;

    public ProvjeriRezervacijuEndpoint(HCIDBContext context,
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
        if (response == null)
            return sobaDostupna;
        return response;
    }
}