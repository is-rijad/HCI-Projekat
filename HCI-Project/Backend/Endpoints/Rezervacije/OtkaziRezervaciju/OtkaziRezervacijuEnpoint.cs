using Backend.Data;
using Backend.Endpoints.Rezervacije.GetBuduceRezervacijeZaGosta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Rezervacije.OtkaziRezervaciju {
    [Route("OtkaziRezervaciju")]
    public class OtkaziRezervacijuEnpoint : BaseEndpoint<OtkaziRezervacijuReq, OtkaziRezervacijuRes> {
        private readonly HCIDBContext _dbContext;

        public OtkaziRezervacijuEnpoint(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpDelete]
        public override async Task<OtkaziRezervacijuRes> Akcija([FromBody]OtkaziRezervacijuReq req)
        {
            var response = new OtkaziRezervacijuRes();
            var rezervacija = await _dbContext.ZauzeteSobe.FirstOrDefaultAsync(zs => zs.Id == req.RezervacijaId);
            if (rezervacija is null)
            {
                response.Status = 404;
                response.Message = "Rezervacija nije pronađena!";
            }

            _dbContext.Remove(rezervacija);
            await _dbContext.SaveChangesAsync();
            response.Message = "Rezervacija je uspješno otkazana.";
            return response;
        }
    }
}
