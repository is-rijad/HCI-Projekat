using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Aranzmani.GetAllZaSobu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Cijene.GetCijenuZaId {
    [Route("Cijene/GetCijenuZaId")]
    public class GetCijenuIdEndpoint : BaseEndpoint<GetCijenuZaIdEndpointReq, GetCijenuZaIdEndpointRes> {
        private readonly HCIDBContext _dbContext;

        public GetCijenuIdEndpoint(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetCijenuZaIdEndpointRes> Akcija([FromQuery] GetCijenuZaIdEndpointReq req)
        {
            var cijene = await _dbContext.Cijene.Where(sa => sa.SobaId == req.Id).OrderByDescending(c => c.BrojOsoba).ToListAsync(); 
            var soba = await _dbContext.Sobe.Where(s => s.Id == req.Id).FirstOrDefaultAsync();
            var responseCijene = new List<Cijena>();
            for (int i = soba.BrojGostiju - 1, j = 0; i >= 0; i--, j++)
            {
                {
                    if (j < cijene.Count)
                    {
                        responseCijene.Add(cijene[j]);
                    }
                    else
                    {
                        responseCijene.Add(new Cijena()
                        {
                            BrojOsoba = i + 1,
                            CijenaSobe = 0,
                            SobaId = soba.Id,
                        });
                    }
                }
            }

            var response = new GetCijenuZaIdEndpointRes();
                if (responseCijene.Count == 0)
                {
                    response.Status = 404;
                    response.Message = "Nije pronađen nijedan aranžman.";
                }

                response.Cijene = responseCijene;
                return response;
        }
    }
}
