using Backend.Data;
using Backend.Data.Modeli;
using Backend.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Sobe.GetSobuId {
    [Route("Sobe/GetSobuId")]
    public class GetSobuIdEndpoint : BaseEndpoint<GetSobuIdEndpointReq, GetSobuIdEndpointRes> {
        private readonly HCIDBContext _dbContext;

        public GetSobuIdEndpoint(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetSobuIdEndpointRes> Akcija([FromQuery] GetSobuIdEndpointReq req)
        {
            var response = new GetSobuIdEndpointRes();
            var soba = await _dbContext.Sobe.Where(s => s.Id == req.Id).FirstOrDefaultAsync();
            if (soba == null)
            {
                response.Status = 404;
                response.Message = "Soba nije pronađena!";
            }

            response.soba = soba;
            return response;
        }
    }
}
