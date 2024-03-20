using Backend.Data;
using Backend.Data.Modeli;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints.Korisnici.Registruj;

[Route("Korisnici/RegistrujSe")]
public class RegistrujKorisnikaEndpoint : BaseEndpoint<RegistrujKorisnikaEndpointReq, RegistrujKorisnikaEndpointRes>
{
    private readonly HCIDBContext _dbContext;
    private readonly Validator _validator;

    public RegistrujKorisnikaEndpoint(HCIDBContext context,
        Validator validator)
    {
        _dbContext = context;
        _validator = validator;
    }

    [HttpPost]
    public override async Task<RegistrujKorisnikaEndpointRes> Akcija(RegistrujKorisnikaEndpointReq req)
    {
        var response = new RegistrujKorisnikaEndpointRes();
        if (!_validator.ValidirajEmail(req.Email)
            || _validator.ValidirajLozinku(req.Lozinka)
            || _validator.ValidirajText(req.Ime)
            || _validator.ValidirajText(req.Prezime)
            || _validator.ValidirajText(req.Grad)
            || _validator.ValidirajText(req.Drzava))
        {
            response.Status = 400;
            response.Message = "Uneseni podaci nisu validni!";
            return response;
        }

        await _dbContext.Gosti.AddAsync(new Gost
        {
            DatumRodjenja = req.DatumRodjenja.Date,
            Email = req.Email,
            Ime = req.Ime,
            Prezime = req.Prezime,
            IsAdmin = false,
            Lozinka = req.Lozinka,
            BrojRezervacija = 0,
            Drzava = req.Drzava,
            Grad = req.Grad,
            SlikaKorisnika = ""
        });
        await _dbContext.SaveChangesAsync();
        return response;
    }
}