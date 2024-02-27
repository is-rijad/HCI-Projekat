using System.IO;
using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Slike.DodajSliku;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Slike.ObrisiSliku {
    [Route("Slike/ObrisiSliku")]
    public class ObrisiSlikuEndpoint : BaseEndpoint<ObrisiSlikuEndpointReq, ObrisiSlikuEndpointRes> {
        private readonly HCIDBContext _dbContext;

        public ObrisiSlikuEndpoint(HCIDBContext context) {
            _dbContext = context;
        }
        [HttpDelete]
        public async override Task<ObrisiSlikuEndpointRes> Akcija([FromBody] ObrisiSlikuEndpointReq req)
        {
            var subStrPocetak = Config.URLAplikacije.Length + 1;
            var subStrKraj = req.urlSlike.Length - subStrPocetak;
            var path = req.urlSlike.Substring(subStrPocetak, subStrKraj);
            var slika = await _dbContext.Slike.Where(s => s.SobaId == req.SobaId).Where(s => s.Path == path).FirstOrDefaultAsync();

            var response = new ObrisiSlikuEndpointRes();
            if (slika == null) {
                response.Status = 404;
                response.Message = "Slika nije pronađena!";
                return response;
            }

            new FileInfo(path).Delete();
            _dbContext.Remove(slika);
            await _dbContext.SaveChangesAsync();
            return response;
        }
    }
}