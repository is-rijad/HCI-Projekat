using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Korisnici.Uloguj;
using Backend.Filteri;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints.Korisnici.Registruj {
    [Route("Korisnici/RegistrujSe")]
    public class RegistrujKorisnikaEndpoint : BaseEndpoint<RegistrujKorisnikaEndpointReq, RegistrujKorisnikaEndpointRes> {
        private readonly HCIDBContext _dbContext;

        public RegistrujKorisnikaEndpoint(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpPost]
        public override async Task<RegistrujKorisnikaEndpointRes> Akcija(RegistrujKorisnikaEndpointReq req)
        {
            var response = new RegistrujKorisnikaEndpointRes();
            await _dbContext.Gosti.AddAsync(new Gost()
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
}
