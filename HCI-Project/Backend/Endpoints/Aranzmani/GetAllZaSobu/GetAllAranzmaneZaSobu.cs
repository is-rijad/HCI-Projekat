using System.Collections.Immutable;
using Backend.Data;
using Backend.Data.Modeli;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Aranzmani.GetAllZaSobu
{
    [Route("Aranzmani/GetZaSobu")]
    public class GetAllAranzmaneZaSobu : BaseEndpoint<int, GetAllAranzmaneZaSobuRes>
    {
        private readonly HCIDBContext _dbContext;

        public GetAllAranzmaneZaSobu(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetAllAranzmaneZaSobuRes> Akcija([FromQuery] int id)
        {
            var sobaAranzman = await _dbContext.SobeAranzmani.Where(sa => sa.SobaId == id).Select(a => a.Aranzman).ToListAsync();
            var sobaAranzmanLista = await _dbContext.SobeAranzmani.Where(sa => sa.SobaId == id).ToListAsync();
            var sviAranzmani = await _dbContext.Aranzmani.ToListAsync();
            var soba = await _dbContext.Sobe.Where(s => s.Id == id).FirstOrDefaultAsync();
            var responseAranzmani = new List<SobaAranzman>();
            foreach (var aranzman in sviAranzmani)
            {
                if(aranzman.NazivAranzmana == "Bez aranžmana")
                    continue;
                var doplata = 0f;
                if (sobaAranzman.Contains(aranzman))
                    doplata = sobaAranzmanLista.Find(sa => sa.AranzmanId == aranzman.Id).Doplata;
                responseAranzmani.Add(new SobaAranzman()
                {
                    Aranzman = aranzman,
                    AranzmanId = aranzman.Id,
                    Doplata = doplata,
                    Soba = soba,
                    SobaId = soba.Id
                });
            }
            var response = new GetAllAranzmaneZaSobuRes();
            if (sobaAranzman.Count == 0)
            {
                response.Status = 404;
                response.Message = "Nije pronađen nijedan aranžman.";
            }
            response.Aranzmani = responseAranzmani.OrderByDescending(a => a.Doplata).ToList();
            return response;
        }
    }
}
