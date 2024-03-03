using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints;
using Backend.Endpoints.Rezervacije.NapraviRezervaciju;
using Microsoft.EntityFrameworkCore;

namespace Backend.Servisi
{
    public class ProvjeriRezervaciju {
        private readonly HCIDBContext _dbContext;

        public ProvjeriRezervaciju(HCIDBContext context)
        {
            _dbContext = context;
        }
        public async Task<BaseResponse> Provjeri(NapraviRezervacijuEndpointReq req) {
            var soba = await _dbContext.Sobe.FirstOrDefaultAsync(s => s.Id == req.SobaId);
            if (soba == null)
                return new BaseResponse() { Message = "Soba nije pronađena!", Status = 404 };

            var rezervacije = await _dbContext.ZauzeteSobe.Where(s => s.SobaId == req.SobaId && s.DatumOdlaska.Date >= DateTime.Today).OrderBy(s => s.DatumDolaska).ThenBy(s => s.DatumOdlaska).ToListAsync();

            if (req.DatumOdlaska.Date <= req.DatumDolaska.Date || req.DatumDolaska < DateTime.Today)
                return new BaseResponse() { Message = "Datumi nisu validni!", Status = 500 };
            if (req.BrojOsoba > soba.BrojGostiju || req.BrojOsoba <= 0)
                return new BaseResponse() { Message = "Broj osoba nije validan!", Status = 500 };


            foreach (var rezervacija in rezervacije) {
                if (rezervacija.DatumDolaska.Date >= req.DatumOdlaska.Date)
                    break;
                if (req.DatumDolaska.Date > rezervacija.DatumDolaska.Date)
                    if (req.DatumDolaska.Date < rezervacija.DatumOdlaska.Date)
                        return new BaseResponse() { Message = "Soba nije dostupna u ovom periodu!", Status = 500 };

                if (req.DatumDolaska.Date < rezervacija.DatumDolaska.Date)
                    if (req.DatumOdlaska.Date > rezervacija.DatumDolaska.Date)
                        return new BaseResponse() { Message = "Soba nije dostupna u ovom periodu!", Status = 500 };

                if (req.DatumDolaska.Date == rezervacija.DatumDolaska.Date)
                    return new BaseResponse() { Message = "Soba nije dostupna u ovom periodu!", Status = 500 };

            }


            var baseCijena = (await _dbContext.Cijene.FirstOrDefaultAsync(c => c.BrojOsoba == req.BrojOsoba))?.CijenaSobe;
            if (baseCijena == null)
                return new BaseResponse() { Message = "Soba nije dostupna za ovaj broj osoba!", Status = 500 };

            var sobaAranzman = await _dbContext.SobeAranzmani.FirstOrDefaultAsync(s => s.Id == req.SobaAranzmanId);

            if (sobaAranzman == null)
                return new BaseResponse() { Message = "Soba nije dostupna za ovaj aranžman!", Status = 500 };
            var doplata = sobaAranzman.Doplata;

            var cijena = (baseCijena * (1 + doplata / 100) + req.BrojDjece * soba.CijenaZaDjecu) *
                         (req.DatumOdlaska - req.DatumDolaska).Days;
            return new ProvjeriRezervacijuEndpointRes()
            {
                DetaljiRezervacije = (new ZauzetaSoba()
                {
                    SobaId = req.SobaId,
                    GostId = 9, //IZMJENA
                    SobaAranzmanId = req.SobaAranzmanId,
                    BrojOsoba = req.BrojOsoba,
                    BrojDjece = req.BrojDjece,
                    Cijena = cijena!.Value,
                    DatumDolaska = req.DatumDolaska.Date,
                    DatumOdlaska = req.DatumOdlaska.Date

                }),
                Soba = soba,
                SobaAranzman = sobaAranzman
            };
        }
        public async Task<bool> Provjeri(Soba soba, NapraviRezervacijuEndpointReq req)
        {

            var rezervacije = await _dbContext.ZauzeteSobe.Where(s => s.SobaId == req.SobaId && s.DatumOdlaska.Date >= DateTime.Today).OrderBy(s => s.DatumDolaska).ThenBy(s => s.DatumOdlaska).ToListAsync();

            foreach (var rezervacija in rezervacije) {
                if (rezervacija.DatumDolaska.Date >= req.DatumOdlaska.Date)
                    break;
                if (req.DatumDolaska.Date > rezervacija.DatumDolaska.Date)
                    if (req.DatumDolaska.Date < rezervacija.DatumOdlaska.Date)
                        return false;
                if (req.DatumDolaska.Date < rezervacija.DatumDolaska.Date)
                    if (req.DatumOdlaska.Date > rezervacija.DatumDolaska.Date)
                        return false;
                if (req.DatumDolaska.Date == rezervacija.DatumDolaska.Date)
                    return false;
            }

            return true;
        }
    }
}
