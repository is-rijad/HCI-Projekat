using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Kreveti.GetAll
{
    [Route("Kreveti/GetAll")]
    public class GetAllKrevete : BaseEndpoint<NoRequest, GetAllKreveteRes>
    {
        private readonly HCIDBContext _dbContext;

        public GetAllKrevete(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public override async Task<GetAllKreveteRes> Akcija([FromQuery] NoRequest req)
        {
            var kreveti = await _dbContext.Kreveti.ToListAsync();
            var response = new GetAllKreveteRes();
            if (kreveti.Count == 0)
            {
                response.Status = 404;
                response.Message = "Nije pronađen nijedan krevet.";
            }
            response.Kreveti = kreveti;
            return response;
        }
    }
}
