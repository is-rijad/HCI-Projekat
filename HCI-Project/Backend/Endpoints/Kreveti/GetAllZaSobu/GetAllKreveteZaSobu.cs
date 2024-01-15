using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Aranzmani.GetAllZaSobu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Kreveti.GetAllZaSobu
{
    [Route("Kreveti/GetZaSobu")]
    public class GetAllKreveteZaSobu : BaseEndpoint<int, GetAllKreveteZaSobuRes>
    {
        private readonly HCIDBContext _dbContext;

        public GetAllKreveteZaSobu(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetAllKreveteZaSobuRes> Akcija([FromQuery] int id)
        {
            var sobaKrevet = await _dbContext.SobeKreveti.Where(sa => sa.SobaId == id).Select(a => a.Krevet).ToListAsync();
            var sobaKrevetLista = await _dbContext.SobeKreveti.Where(sa => sa.SobaId == id).ToListAsync();
            var sviKreveti = await _dbContext.Kreveti.ToListAsync();
            var soba = await _dbContext.Sobe.Where(s => s.Id == id).FirstOrDefaultAsync();
            var responseKreveti = new List<SobaKrevet>();
            foreach (var krevet in sviKreveti)
            {
                var kreveti = 0;
                if (sobaKrevet.Contains(krevet))
                    kreveti = sobaKrevetLista.Find(sa => sa.KrevetId == krevet.Id).BrojKreveta;
                responseKreveti.Add(new SobaKrevet()
                {
                    Krevet = krevet,
                    KrevetId = krevet.Id,
                    BrojKreveta = kreveti,
                    Soba = soba,
                    SobaId = soba.Id
                });
            }
            var response = new GetAllKreveteZaSobuRes();
            if (responseKreveti.Count == 0)
            {
                response.Status = 404;
                response.Message = "Nije pronađen nijedan aranžman.";
            }
            response.Kreveti = responseKreveti;
            return response;
        }
    }
}
