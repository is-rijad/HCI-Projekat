using Backend.Filteri;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints.Korisnici.Odjavi;

[Route("Korisnici/OdjaviSe")]
public class OdjaviKorisnikaEndpoint : BaseEndpoint<NoRequest, OdjaviKorisnikaEndpointRes>
{
    private readonly AuthServis _authServis;

    public OdjaviKorisnikaEndpoint(AuthServis authServis)
    {
        _authServis = authServis;
    }

    [AuthFilter]
    [HttpGet]
    public override async Task<OdjaviKorisnikaEndpointRes> Akcija([FromQuery] NoRequest req)
    {
        var response = new OdjaviKorisnikaEndpointRes();
        var rezultat = await _authServis.OdjaviKorisnika();
        if (!rezultat)
        {
            response.Status = 500;
            response.Message = "Odjava nije uspješna!";
        }

        return response;
    }
}