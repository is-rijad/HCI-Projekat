using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.Data;
using Backend.Data.Modeli;
using Backend.Filteri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Endpoints.Rezervacije.GetBuduceRezervacijeZaGosta {
    [Route("GetBuduceRezervacijeZaGosta")]
    public class GetBuduceRezervacijeZaGostaEndpoint : BaseEndpoint<NoRequest, GetBuduceRezervacijeZaGostaEndpointRes> {
        private readonly HCIDBContext _dbContext;

        public GetBuduceRezervacijeZaGostaEndpoint(HCIDBContext context)
        {
            _dbContext = context;
        }
        [AuthFilter]

        [HttpGet]
        public override async Task<GetBuduceRezervacijeZaGostaEndpointRes> Akcija([FromQuery]NoRequest req)
        {
            
            var token = JsonConvert.DeserializeObject<Tokeni>(HttpContext.Request.Cookies["auth-token"]!);
            var gostId = (await _dbContext.Tokeni.FirstOrDefaultAsync(t => t.Token == token.Token)).KorisnickiNalogId;
            var response = new GetBuduceRezervacijeZaGostaEndpointRes();
            var rezervacije = await _dbContext.ZauzeteSobe
                .Where(zs => zs.GostId == gostId && zs.DatumDolaska.Date > DateTime.Today).ToListAsync();
            if (rezervacije.Count == 0)
            {
                response.Status = 404;
                response.Message = "Nije pronađena nijedna buduća rezervacija!";
            }
            response.Rezervacije = new List<RezervacijaModel>();
            foreach (var rezervacija in rezervacije)
            {
                response.Rezervacije.Add(new RezervacijaModel()
                {
                    Aranzman = rezervacija!.SobaAranzman!.Aranzman!.NazivAranzmana,
                    NazivSobe = rezervacija.Soba!.NazivSobe,
                    OpisSobe = rezervacija.Soba.Opis,
                    Rezervacija = rezervacija
                });
            }
            return response;

        }
    }
}
