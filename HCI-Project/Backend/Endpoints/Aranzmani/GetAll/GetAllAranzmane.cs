using Backend.Data;
using Backend.Filteri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Aranzmani.GetAll
{
    [Route("Aranzmani/GetAll")]
    public class GetAllAranzmane : BaseEndpoint<NoRequest, GetAllAranzmaneRes>
    {
        private readonly HCIDBContext _dbContext;

        public GetAllAranzmane(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetAllAranzmaneRes> Akcija([FromQuery] NoRequest req)
        {
            var aranzmani = await _dbContext.Aranzmani.OrderBy(a => a.NazivAranzmana).ToListAsync();
            var response = new GetAllAranzmaneRes();
            if (aranzmani.Count == 0)
            {
                response.Status = 404;
                response.Message = "Nije pronađen nijedan aranžman.";
            }
            response.Aranzmani = aranzmani;
            return response;
        }
    }
}
