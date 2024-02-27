using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Sobe.GetSlikeZaSobu
{
    [Route("Soba/GetSlikeZaSobu")]
    public class GetSlikeZaSobuEndpoint : BaseEndpoint<GetSlikeZaSobuEndpointReq, GetSlikeZaSobuEndpointRes>
    {
        private readonly HCIDBContext _dbContext;

        public GetSlikeZaSobuEndpoint(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetSlikeZaSobuEndpointRes> Akcija([FromQuery] GetSlikeZaSobuEndpointReq req)
        {
            var rezultat = await _dbContext.Slike.Where(s => s.SobaId == req.SobaId).Select(s => s.Path).ToListAsync();
            var response = new GetSlikeZaSobuEndpointRes();

            if (rezultat.Count == 0)
            {
                response.Status = 404;
                response.Message = "Nijedna slika nije pronađena!";
                return response;
            }

            response.Slike = rezultat;
            return response;
        }
    }
}
