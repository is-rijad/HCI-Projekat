using Backend.Data;
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
            var response = new GetCijenuZaIdEndpointRes();
            var cijene = await _dbContext.Cijene.Where(s => s.Id == req.Id).ToListAsync();
            if (cijene.Count == 0)
            {
                response.Status = 404;
                response.Message = "Soba nije pronađena!";
            }

            response.Cijene = cijene;
            return response;
        }
    }
}
