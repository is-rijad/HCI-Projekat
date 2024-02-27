using Backend.Data;
using Backend.Data.Modeli;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Endpoints.Slike.DodajSliku {
    [Route("Slike/DodajSliku")]
    public class DodajSlikuEndpoint : BaseEndpoint<DodajSlikuEndpointReq, DodajSlikuEndpointRes> {
        private readonly HCIDBContext _dbContext;

        public DodajSlikuEndpoint(HCIDBContext context)
        {
            _dbContext = context;
        }
        [HttpPost]
        public async override Task<DodajSlikuEndpointRes> Akcija([FromForm] DodajSlikuEndpointReq req)
        {
            string path = $"{Config.FolderSlike}/{req.SobaId}";
            string ekstenzija = Path.GetExtension(req.Slika.FileName);
            var soba = await _dbContext.Sobe.FirstOrDefaultAsync(s => s.Id == req.SobaId);
            var response = new DodajSlikuEndpointRes();
            if (soba == null) 
            {
                response.Status = 404;
                response.Message = "Soba nije pronađena!";
                return response;
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var guid = Guid.NewGuid().ToString();
            using (FileStream fs = new FileStream($"{path}/{guid}{ekstenzija}", FileMode.Create)) {
                req.Slika.CopyTo(fs);
            }
            
            await _dbContext.Slike.AddAsync(new Slika()
            {
                SobaId = soba.Id,
                Path = $"{path}/{guid}{ekstenzija}"
            });
            await _dbContext.SaveChangesAsync();
            return response;
        }
    }
}
