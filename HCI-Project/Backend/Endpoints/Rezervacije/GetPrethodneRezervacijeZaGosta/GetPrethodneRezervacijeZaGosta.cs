using System.Reflection.Metadata.Ecma335;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Rezervacije.GetBuduceRezervacijeZaGosta {
    [Route("GetPrethodneRezervacijeZaGosta")]
    public class GetPrethodneRezervacijeZaGosta : BaseEndpoint<GetPrethodneRezervacijeZaGostaReq, GetPrethodneRezervacijeZaGostaRes> {
        private readonly HCIDBContext _dbContext;

        public GetPrethodneRezervacijeZaGosta(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetPrethodneRezervacijeZaGostaRes> Akcija([FromQuery]GetPrethodneRezervacijeZaGostaReq req)
        {
            var response = new GetPrethodneRezervacijeZaGostaRes();
            var rezervacije = await _dbContext.ZauzeteSobe
                .Where(zs => zs.GostId == req.GostId && zs.DatumDolaska.Date <= DateTime.Today).ToListAsync();
            if (rezervacije.Count == 0)
            {
                response.Status = 404;
                response.Message = "Nije pronađena nijedna rezervacija!";
            }
            response.Rezervacije = new List<RezervacijaModel>();
            foreach (var rezervacija in rezervacije) {
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
