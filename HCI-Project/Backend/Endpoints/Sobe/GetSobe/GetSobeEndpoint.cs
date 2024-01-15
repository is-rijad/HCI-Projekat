using Backend.Data;
using Backend.Data.Modeli;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Sobe.GetSobe
{
    [Route("Sobe/GetSobe")]
    public class GetSobeEndpoint : BaseEndpoint<GetSobeReq, GetSobeRes>
    {
        private readonly HCIDBContext _dbContext;

        public GetSobeEndpoint(HCIDBContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public override async Task<GetSobeRes> Akcija([FromBody] GetSobeReq req)
        {
            var sobe = new GetSobeRes()
            {
                Sobe = _dbContext.Sobe.Select((soba) => new PregledSobeRes()
                {
                    Id = soba.Id,
                    BrojGostiju = soba.BrojGostiju,
                    NazivSobe = soba.NazivSobe,
                    Opis = soba.Opis,
                    Slike = soba.Slike
                }).ToList()
            };

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
