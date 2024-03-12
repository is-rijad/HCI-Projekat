using Backend.Data;
using Backend.Data.Modeli;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Cijene.GetCijenuZaId;

[Route("Cijene/GetCijenuZaId")]
public class GetCijenuIdEndpoint : BaseEndpoint<GetCijenuZaIdEndpointReq, GetCijenuZaIdEndpointRes>
{
    private readonly HCIDBContext _dbContext;

    public GetCijenuIdEndpoint(HCIDBContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    public override async Task<GetCijenuZaIdEndpointRes> Akcija([FromQuery] GetCijenuZaIdEndpointReq req)
    {
        var responseCijene = new List<Cijena>();

        if (req.Id == 0)
        {
            for (var i = req.BrojOsoba; i >= 1; i--)
            {
                responseCijene.Add(new Cijena
                {
                    BrojOsoba = i,
                    CijenaSobe = 0,
                    SobaId = req.Id
                });
            }
        }
        else
        {
            var cijene = await _dbContext.Cijene.Where(sa => sa.SobaId == req.Id).OrderByDescending(c => c.BrojOsoba)
                .ToListAsync();
            if (cijene.Count > req.BrojOsoba)
            {
                var posljednjiIndex = cijene.Count - req.BrojOsoba;
                for (int i = 0, j = 0; i < posljednjiIndex; i++)
                {
                    cijene.RemoveAt(j);
                }
            }
            else
            {
                for (var i = cijene.Count; i < req.BrojOsoba; i++)
                {
                    cijene.Add(new Cijena
                    {
                        BrojOsoba = i + 1,
                        CijenaSobe = 0,
                        SobaId = req.Id
                    });
                }
            }


            responseCijene = cijene.OrderByDescending(c => c.BrojOsoba).ToList();
        }


        var response = new GetCijenuZaIdEndpointRes();
        if (responseCijene.Count == 0)
        {
            response.Status = 404;
            response.Message = "Nije pronađena nijedna cijena.";
        }

        response.Cijene = responseCijene;
        return response;
    }
}