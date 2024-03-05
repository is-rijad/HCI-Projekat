using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Rezervacije.NapraviRezervaciju;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Sobe.GetSobe
{
    [Route("Sobe/GetSobe")]
    public class GetSobeEndpoint : BaseEndpoint<GetSobeReq, GetSobeRes>
    {
        private readonly HCIDBContext _dbContext;
        private readonly ProvjeriRezervaciju _provjeriRezervaciju;

        public GetSobeEndpoint(HCIDBContext context,
            ProvjeriRezervaciju provjeriRezervaciju)
        {
            _dbContext = context;
            _provjeriRezervaciju = provjeriRezervaciju;
        }

        [HttpPost]
        public override async Task<GetSobeRes> Akcija([FromBody] GetSobeReq req)
        {
            var sveSobe = await _dbContext.Sobe.Select(s => s.Id).ToListAsync();
            var sobe = new GetSobeRes();
            foreach (var soba in sveSobe)
            {
                var rezultat = await _provjeriRezervaciju.Provjeri(new ProvjeriRezervacijuEndpointReq()
                {
                    SobaId = soba,
                    BrojDjece = req.BrojDjece,
                    BrojOsoba = req.BrojOdraslih,
                    DatumDolaska = req.DatumPrijave,
                    DatumOdlaska = req.DatumOdjave,
                    SobaAranzmanId = null
                });
                if (rezultat is ProvjeriRezervacijuEndpointRes)
                {
                    var rezultatRes = rezultat as ProvjeriRezervacijuEndpointRes;
                    sobe.Sobe.Add(new PregledSobeRes()
                    {
                        Id = rezultatRes!.Soba!.Id,
                        BrojGostiju = req.BrojOdraslih,
                        Cijena = rezultatRes.DetaljiRezervacije!.Cijena,
                        NazivSobe = rezultatRes.Soba.NazivSobe,
                        Opis = rezultatRes.Soba.Opis
                    });
                }
            }
            if (sobe.Sobe.Count == 0)
            {
                sobe.Status = 404;
                sobe.Message = "Nažalost, nije pronađena nijedna soba.";
            }

            return sobe;
        }

        private async Task<List<Soba>> GetDostupneSobe(GetSobeReq request)
        {
            var sobe = await _dbContext.Sobe.ToListAsync();
            var zauzete = await _dbContext.ZauzeteSobe.Include(zs => zs.Soba).Where(zs => !DostupnaSoba(zs, request)).ToListAsync();
            
            var dostupneSobe = new List<Soba>();

            foreach (var soba in sobe)
            {
                var isZauzeta = false;
                foreach (var zauzeta in zauzete)
                {
                    if (soba.Id == zauzeta.SobaId)
                    {
                        isZauzeta = true; 
                        break;
                    }
                }
                if (!isZauzeta)
                    dostupneSobe.Add(soba);
            }
            return dostupneSobe;
        }

    private bool DostupnaSoba(ZauzetaSoba soba, GetSobeReq request)
        {
            if (soba.DatumDolaska < request.DatumPrijave) {
                if (soba.DatumOdlaska > request.DatumPrijave)
                    return false;

            }

            else if (soba.DatumDolaska > request.DatumPrijave)
            {
                if (soba.DatumDolaska < request.DatumOdjave)
                    return false;
            }
            else return false;

            return true;
        }
    }
}
