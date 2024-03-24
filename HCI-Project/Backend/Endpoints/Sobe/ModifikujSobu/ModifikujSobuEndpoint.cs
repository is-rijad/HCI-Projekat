using Backend.Data;
using Backend.Data.Modeli;
using Backend.Filteri;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Sobe.ModifikujSobu;

[Route("Sobe/ModifikujSobu")]
public class ModifikujSobuEndpoint : BaseEndpoint<ModifikujSobuEndpointReq, ModifikujSobuEndpointRes>
{
    private readonly HCIDBContext _dbContext;
    private readonly Validator _validator;

    public ModifikujSobuEndpoint(HCIDBContext context, Validator validator)
    {
        _dbContext = context;
        _validator = validator;
    }

    [AuthFilter]
    [MenadzerFilter]
    [HttpPost]
    public override async Task<ModifikujSobuEndpointRes> Akcija([FromBody] ModifikujSobuEndpointReq req)
    {
        var response = new ModifikujSobuEndpointRes();
        if (!ValidirajUnos(req))
        {
            response.Status = 400;
            response.Message = "Unos nije validan!";
            return response;
        }

        Soba? soba = null;
        if (req.Soba.Id != 0)
        {
            soba = await _dbContext.Sobe.Where(s => s.Id == req.Soba.Id).FirstOrDefaultAsync();
            if (soba == null)
            {
                response.Status = 404;
                response.Message = "Soba nije pronađena!";
                return response;
            }
        }
        else
        {
             req.Soba.Id = null;
             req.Soba.Aranzmani = null;
             req.Soba.Cijene = null;
             req.Soba.ZauzetaSoba = null;
             req.Soba.Kreveti = null;
             soba = (await _dbContext.Sobe.AddAsync(req.Soba)).Entity;
             await _dbContext.SaveChangesAsync();
        }

        await Task.Run(() => ObrisiStarePodatke(req));

        req.Aranzmani.ForEach(a =>
        {
            a.Id = null;
            a.SobaId = soba.Id.Value;
            a.Aranzman = null;
        });

        req.Kreveti.ForEach(a =>
        {
            a.Id = null;
            a.SobaId = soba.Id.Value;
            a.Krevet = null;
        });
        req.Cijene.ForEach(a =>
        {
            a.Id = null;
            a.SobaId = soba.Id.Value;
        });
        await _dbContext.SobeAranzmani.AddRangeAsync(req.Aranzmani);
        await _dbContext.SobeKreveti.AddRangeAsync(req.Kreveti);
        await _dbContext.Cijene.AddRangeAsync(req.Cijene);

        await _dbContext.SaveChangesAsync();
        response.Message = "Modifikacija je uspješno izvršena.";
        return response;
    }

    private async Task ObrisiStarePodatke(ModifikujSobuEndpointReq req)
    {
        await _dbContext.SobeKreveti.Where(sk => sk.SobaId == req.Soba.Id).ExecuteDeleteAsync();
        await _dbContext.SobeAranzmani.Where(sk => sk.SobaId == req.Soba.Id).ExecuteDeleteAsync();
        await _dbContext.Cijene.Where(sk => sk.SobaId == req.Soba.Id).ExecuteDeleteAsync();
    }

    private bool ValidirajUnos(ModifikujSobuEndpointReq req)
    {
        if (req.Soba.BrojGostiju < 1)
            return false;
        if (!_validator.ValidirajText(req.Soba.NazivSobe)
            || !_validator.ValidirajText(req.Soba.Opis))
            return false;
        if (req.Cijene.Count < req.Soba.BrojGostiju)
            return false;
        return true;
    }
}