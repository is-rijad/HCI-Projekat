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
            var soba = await _dbContext.Sobe.FirstOrDefaultAsync(s => s.Id == req.SobaId);
            if (soba == null)
                return new BaseResponse() { Message = "Soba nije pronađena!", Status = 404 };

            var rezervacije = await _dbContext.ZauzeteSobe.Where(s => s.SobaId == req.SobaId && s.DatumOdlaska.Date >= DateTime.Today).OrderBy(s => s.DatumDolaska).ThenBy(s => s.DatumOdlaska).ToListAsync();

            if (req.DatumOdlaska.Date <= req.DatumDolaska.Date || req.DatumDolaska < DateTime.Today)
                return new BaseResponse() { Message = "Datumi nisu validni!", Status = 500 };
            if (req.BrojOsoba > soba.BrojGostiju || req.BrojOsoba <= 0)
                return new BaseResponse() { Message = "Broj osoba nije validan!", Status = 500 };

            if (!await _provjeriRezervaciju.Provjeri(soba, req))
                return new BaseResponse() { Message = "Soba nije dostupna u ovom periodu!", Status = 500 };


            var baseCijena = (await _dbContext.Cijene.FirstOrDefaultAsync(c => c.BrojOsoba == req.BrojOsoba))?.CijenaSobe;
            if (baseCijena == null)
                return new BaseResponse() { Message = "Soba nije dostupna za ovaj broj osoba!", Status = 500 };

            var doplata = (await _dbContext.SobeAranzmani.FirstOrDefaultAsync(s => s.Id == req.SobaAranzmanId))?.Doplata;
            if (doplata == null)
                return new BaseResponse() { Message = "Soba nije dostupna za ovaj aranžman!", Status = 500 };


            var cijena = (baseCijena * (1 + doplata / 100) + req.BrojDjece * soba.CijenaZaDjecu) *
                         (req.DatumOdlaska - req.DatumDolaska).Days;
            await _dbContext.ZauzeteSobe.AddAsync(new ZauzetaSoba()
            {
                SobaId = req.SobaId,
                GostId = 9, //IZMJENA
                SobaAranzmanId = req.SobaAranzmanId,
                BrojOsoba = req.BrojOsoba,
                BrojDjece = req.BrojDjece,
                Cijena = cijena!.Value,
                DatumDolaska = req.DatumDolaska.Date,
                DatumOdlaska = req.DatumOdlaska.Date

            });
            await _dbContext.SaveChangesAsync();
            return new BaseResponse() { Message = "Rezervacija je uspješno napravljena!" };
        }
    }
}
